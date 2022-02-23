using A.Blockchain.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Core.Interfaces.DbContext
{
    public interface IBlockchainDbContext
    {
        IQueryable<TEntity> GetAll<TEntity>();
        void Add<TEntity>(TEntity entity);
        void Update<TEntity>(TEntity entity);
        void Delete<TEntity>(TEntity entity) where TEntity : BaseDomainObject;
    }
}
