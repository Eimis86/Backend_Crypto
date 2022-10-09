using Cripto_Api.Contract;
using Cripto_Api.Data;

namespace Cripto_Api.Repository
{
    public class GenericRepository<T> : IGenericContract<T> where T : class
    {
        public readonly CriptoAPIDbContext _database;
        public GenericRepository(CriptoAPIDbContext database)
        {
            _database = database;
        }
        public async Task<T> GetAsync(int? id)
        {
            if(id == null)
            {
                return null;
            }
            
            return await _database.Set<T>().FindAsync(id);
        }
        public async Task<T> AddAsync(T entiyty)
        {

            await _database.Set<T>().AddAsync(entiyty);
            await _database.SaveChangesAsync();
            return entiyty;
        }
        public async Task UpdateAsync(T entity)
        {
            _database.Update(entity);
            await _database.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            _database.Set<T>().Remove(entity);
            await _database.SaveChangesAsync();
        }
        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }
    }
}
