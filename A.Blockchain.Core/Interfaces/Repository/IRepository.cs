using A.Blockchain.Core.Domain;

namespace A.Blockchain.Core.Interfaces.Repository
{
    public interface IRepository<T> where T : BaseDomainObject
    {
        T Add(T entity);
        bool Delete(T entity);
        bool DeleteAll(params int[] transactionIds);
        T Update(T entity);

        IEnumerable<T> GetAll();
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        IEnumerable<T> UpdateRange(IEnumerable<T> entities);
    }
}
