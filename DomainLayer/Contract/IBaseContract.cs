using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contract
{
    public interface IBaseContract<T> //: IBaseContract2<T>
    {
        Task<T> AddAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<IQueryable<T>> GetAllAsync();
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int id);
       // Task<IEnumerable<T>> GetFiltered(Expression<Func<T, bool>> filtered);

        //Task<IEnumerable<T>> GetSorted<TKey>(Expression<Func<T, TKey>> sorted); //bool descendingOrder);
    }
}
