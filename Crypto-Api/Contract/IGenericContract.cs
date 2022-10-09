namespace Cripto_Api.Contract
{
    public interface IGenericContract<T> where T : class
    {
        Task<T> GetAsync(int? id);
        Task<T> AddAsync(T entiyty);
        Task UpdateAsync( T entiyty);
        Task DeleteAsync(int id);
        Task<bool> Exists (int id);
    }
}
