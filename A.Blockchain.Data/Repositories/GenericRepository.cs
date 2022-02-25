using A.Blockchain.Core.Domain;
using A.Blockchain.Core.Interfaces.DbContext;
using A.Blockchain.Core.Interfaces.Repository;
using System.Linq;


namespace A.Blockchain.Data.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : BaseDomainObject
    {
        private readonly IBlockchainDbContext dbContext;

        public GenericRepository(IBlockchainDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public T Add(T entity)
        {
            this.dbContext.Add(entity);

            return entity;
        }

        public bool Delete(T entity)
        {
            this.dbContext.Delete(entity);

            return true;
        }

        public bool DeleteAll(params int[] transactionIds)
        {
            this.dbContext.DeleteAll<T>(transactionIds);

            return true;
        }

        public T Update(T entity)
        {
            this.dbContext.Update(entity);

            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            return this.dbContext.GetAll<T>();
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            foreach(var entity in entities)
            {
                this.dbContext.Add(entity);

                yield return entity;
            }
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            foreach(var entity in entities)
            {
                this.dbContext.Update(entity);

                yield return entity;
            }
        }
    }
}
