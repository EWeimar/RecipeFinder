using System.Collections.Generic;

namespace RecipeFinder.DataLayer.OldRepositories
{
    public interface IRepository<T>
    {
        T Create(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(string propertyName, object value);
        void Update(T entity);
        void Delete(int id);
    }
}
