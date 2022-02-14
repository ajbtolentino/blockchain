using A.Blockchain.Core.Interfaces.Repository;
using A.Blockchain.Core.Interfaces.Service;
using A.Blockchain.Data.Repositories;
using A.Blockchain.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Infrastructure.Extensions
{
    public static class DependencyExtension
    {
        public static void UseBlockchainDependencies(this IServiceCollection services)
        {
            //Add db context
            //services.AddSingleton<IDbContext, SqlDbContext>();

            //Add repositories
            services.AddSingleton<IBlockRepository, BlockRepository>();

            //Add services
            services.AddTransient<IBlockchainService, BlockchainService>();
        }
    }
}
