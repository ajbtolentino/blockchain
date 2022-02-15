using A.Blockchain.Core.Domain;
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
        protected static List<T> _repositoryData = new List<T>();

        public T Add(T entity)
        {
            _repositoryData.Add(entity);

            return entity;
        }

        public T Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return _repositoryData;
        }

        public T Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
