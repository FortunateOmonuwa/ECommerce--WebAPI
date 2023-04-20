using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contract
{
    public interface IBaseContractNR<T>
    {
        Task<T> AddNRAsync(T entity, int id);
        Task<T> GetNRByIdAsync(int id, int firstParam);
        Task<IQueryable<T>> GetAllNRAsync(int id);
        Task<T> UpdateNRAsync(T entity, int firstParam);
        Task<T> DeleteNRAsync(int id, int secondParam);
    }
}
