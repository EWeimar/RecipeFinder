using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DataLayer.Repositories
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
