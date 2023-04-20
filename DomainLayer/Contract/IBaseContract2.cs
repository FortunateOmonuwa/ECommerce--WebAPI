using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contract
{
    public interface IBaseContract2<T>
    {
        Task<T> AddNestedAsync(T entity, int id);
        Task<T> GetNestedByIdAsync(int id, int firstParam);
        Task<IQueryable<T>> GetAllNestedAsync(int id);
        Task<T> UpdateNestedAsync(T entity, int firstParam);
        Task<T> DeleteNestedAsync(int id, int secondParam);
    }
}
