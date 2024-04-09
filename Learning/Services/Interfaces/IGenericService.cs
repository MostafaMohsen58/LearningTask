using Learning.Dto;
using Learning.Models;

namespace Learning.Services.Interfaces
{
    public interface IGenericService<T,T2> where T : class
    {
        Task AddAsync(T model);
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task UpdateAsync(Guid id, T model, T2 dto);

        Task DeleteAsync(Guid id);
    }
}
