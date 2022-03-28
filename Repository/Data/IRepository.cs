using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public interface IRepository<T, TPrimaryKey> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Get(TPrimaryKey id);
        Task<T> Add(T entity);
        Task<T> Update(TPrimaryKey id, T entity);
        Task<T> Delete(TPrimaryKey id);
    }
}
