using AvainParkKlere.Api.EntityFrameworkCore;
using AvainParkKlere.Api.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace AvainParkKlere.Api.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AvianParkDbContext apDbContext;

        public GenericRepository(AvianParkDbContext ApDbContext)
        {
            apDbContext = ApDbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await apDbContext.AddAsync(entity);
            await apDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if (entity != null)
            {
                apDbContext.Set<T>().Remove(entity);
                await apDbContext.SaveChangesAsync();   
            }
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await apDbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await apDbContext.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            apDbContext.Update(entity);
            await apDbContext.SaveChangesAsync();
        }

    }

}
