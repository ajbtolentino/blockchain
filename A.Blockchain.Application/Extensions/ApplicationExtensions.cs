using A.Blockchain.Application.Abstractions.Commands;
using A.Blockchain.Application.Abstractions.Queries;
using A.Blockchain.Application.Commands;
using A.Blockchain.Application.Queries;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace A.Blockchain.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            //Add Commands, Queries, Handlers
            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

            services.Scan(source => source.FromAssemblies(assembly)
                                            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                                            .AsImplementedInterfaces()
                                            .WithScopedLifetime());

            services.Scan(source => source.FromAssemblies(assembly)
                                            .AddClasses(c => c.AssignableTo(typeof(ICommandResultHandler<,>)))
                                            .AsImplementedInterfaces()
                                            .WithScopedLifetime());

            services.AddSingleton<IQueryDispatcher, QueryDispatcher>();

            services.Scan(s => s.FromAssemblies(assembly)
                                            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                                            .AsImplementedInterfaces()
                                            .WithScopedLifetime());

            return services;
        }
    }
}
