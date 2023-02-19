using Microsoft.EntityFrameworkCore;
using SocionicTeamBuilder.DAL.EF;
using SocionicTeamBuilder.DAL.Interfaces;

namespace SocionicTeamBuilder.DAL.Repositories
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly SocionicTeamBuilderContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public EFGenericRepository(SocionicTeamBuilderContext context)
        {
            dbContext = context;
            dbSet = context.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity item)
        {
            dbSet.AsNoTracking();
            await dbSet.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            dbSet.AsNoTracking();
            TEntity entity = await dbSet.FindAsync(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
            }
        }

        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            dbSet.AsNoTracking();
            return dbSet.Where(predicate).ToList();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            dbSet.AsNoTracking();
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            dbSet.AsNoTracking();
            return await dbSet.ToListAsync();
        }

        public void Update(TEntity item)
        {
            dbSet.AsNoTracking();
            dbContext.Entry(item).State = EntityState.Modified;
        }
    }
}
