namespace A.Blockchain.Core.Interfaces.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Add(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
