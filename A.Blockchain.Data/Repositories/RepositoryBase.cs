using A.Blockchain.Core.Domain;
using A.Blockchain.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Data.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : BaseDomainObject
    {
        protected static Dictionary<int, T> _repositoryData = new Dictionary<int, T>();

        public T Add(T entity)
        {
            entity.Id = (int)DateTime.UtcNow.ToFileTime();

            _repositoryData.Add(entity.Id, entity);

            return entity;
        }

        public bool Delete(T entity)
        {
            return _repositoryData.Remove(entity.Id);
        }

        public T Update(T entity)
        {
            _repositoryData[entity.Id] = entity;

            return _repositoryData[entity.Id];
        }

        public IEnumerable<T> GetAll()
        {
            return _repositoryData.Values;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            foreach(var entity in entities)
            {
                _repositoryData.Add(entity.Id, entity);

                yield return entity;
            }
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            foreach(var entity in entities)
            {
                _repositoryData[entity.Id] = entity;

                yield return _repositoryData[entity.Id];
            }
        }
    }
}
