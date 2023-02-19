namespace SocionicTeamBuilder.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(int id);
        IEnumerable<TEntity> Find(Func<TEntity, Boolean> predicate);
        Task CreateAsync(TEntity item);
        void Update(TEntity item);
        Task DeleteAsync(int id);
    }
}
