using A.Blockchain.Domain.Entities;

namespace A.Blockchain.Domain.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        T Add(T entity);
        bool Delete(T entity);
        T Update(T entity);

        IEnumerable<T> GetAll();
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        IEnumerable<T> UpdateRange(IEnumerable<T> entities);
    }
}
