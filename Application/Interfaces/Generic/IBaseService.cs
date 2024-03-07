using Domain.Entities;

namespace Application.Interfaces.Generic
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);        
        Task<TEntity> GetById(int id, CancellationToken cancellationToken);
        Task<List<TEntity>> GetAll(CancellationToken cancellationToken);
        IQueryable<TEntity> Get();
    }
}
