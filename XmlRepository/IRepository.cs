using System.Linq;

namespace XmlRepository
{
    public interface IRepository<T>
    {
        T GetById(int id);

        T Insert(T item);

        T Update(T item);

        void Delete(T item);

        IQueryable<T> Table { get; }
    }
}
