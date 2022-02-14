using A.Blockchain.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Data.Repositories
{
    public class RepositoryBase<T> : IRepository<T>
    {
        public T Add(T entity)
        {
            throw new NotImplementedException();
        }

        public T Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
