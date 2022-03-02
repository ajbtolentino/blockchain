using A.Blockchain.Domain.Entities;

namespace A.Blockchain.Application.Common.Interfaces
{
    public interface IBlockchainDbContext
    {
        IQueryable<TEntity> GetAll<TEntity>();
        void Add<TEntity>(TEntity entity);
        void Update<TEntity>(TEntity entity);
        void Delete<TEntity>(TEntity entity) where TEntity : EntityBase;
    }
}
