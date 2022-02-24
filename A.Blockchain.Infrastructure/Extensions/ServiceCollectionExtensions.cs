using A.Blockchain.Core.Domain;
using A.Blockchain.Core.Interfaces.DbContext;
using A.Blockchain.Core.Interfaces.Repository;
using A.Blockchain.Core.Interfaces.Service;
using A.Blockchain.Data.DbContext;
using A.Blockchain.Data.Repositories;
using A.Blockchain.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using A.Blockchain.Core.Interfaces;
using A.Blockchain.Infrastructure.Mapper;

namespace A.Blockchain.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void UseBlockchainDependencies(this IServiceCollection services)
        {
            //Add db context
            //services.AddSingleton<IDbContext, SqlDbContext>();

            //Add repositories
            services.AddSingleton<IBlockRepository, BlockRepository>();
            services.AddSingleton<IRepository<Transaction>, GenericRepository<Transaction>>();


            //Add services
            services.AddTransient<IBlockchainService, BlockchainService>();
            services.AddTransient<IMinerService, MinerService>();
            services.AddTransient<INodeService, NodeService>();
            services.AddTransient<IWalletService, WalletService>();
            services.AddTransient<IHashService, HashService>();

            //DbContext
            services.AddTransient<IBlockchainDbContext, BlockchainLiteDbContext>();

            //AutoMapper
            services.AddTransient<IObjectMapper, BlockchainAutoMapper>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
