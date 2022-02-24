using A.Blockchain.Core.Domain;
using A.Blockchain.Core.Interfaces.DbContext;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Data.DbContext
{
    public class BlockchainLiteDbContext : IBlockchainDbContext
    {
        private readonly LiteDatabase liteDatabase;

        public BlockchainLiteDbContext()
        {
            liteDatabase = new LiteDatabase(@$"Filename=BlockchainDb.db; Connection=Shared");

            liteDatabase.Mapper.Entity<Transaction>().Id(_ => _.Id);
            liteDatabase.Mapper.Entity<Block>().Id(_ => _.Id);
        }

        public void Add<TEntity>(TEntity entity)
        {
            this.liteDatabase.GetCollection<TEntity>().Insert(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity:BaseDomainObject
        {
            this.liteDatabase.GetCollection<TEntity>().Delete(entity.Id);
        }

        public IQueryable<TEntity> GetAll<TEntity>()
        {
            return liteDatabase.GetCollection<TEntity>().FindAll().AsQueryable();
        }

        public void Update<TEntity>(TEntity entity)
        {
            liteDatabase.GetCollection<TEntity>().Upsert(entity);
        }
    }
}
