using Microsoft.Extensions.DependencyInjection;
using A.Blockchain.Infrastructure.Repositories;
using A.Blockchain.Domain.Repositories;
using A.Blockchain.Domain.Entities;
using A.Blockchain.Application.Common.Interfaces;
using A.Blockchain.Infrastructure.DbContext;
using A.Blockchain.Infrastructure.Extensions;

namespace A.Blockchain.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            //Add repositories
            services.AddSingleton<IBlockRepository, BlockRepository>();
            services.AddSingleton<IRepository<Transaction>, GenericRepository<Transaction>>();

            //DbContext
            services.AddTransient<IBlockchainDbContext, BlockchainLiteDbContext>();

            return services;
        }
    }
}
