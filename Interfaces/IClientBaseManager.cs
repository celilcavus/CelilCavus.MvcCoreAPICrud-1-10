namespace CelilCavus.Interfaces
{
    public interface IClientBaseManager<TEntity> where TEntity : class
    {
        ICollection<TEntity> GetAll();
        Task<ICollection<TEntity>> GetAllAsycn();
        TEntity GetById(int id);

        Task<TEntity> GetByIdAsycn(int id);

        void Url(string baseUrl, string url);
        Task Add(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
    }
}