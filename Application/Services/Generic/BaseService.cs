using Application.Interfaces.Generic;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services.Generic
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _repository;

        public BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public void Create(TEntity entity)
        {
            _repository.Create(entity);
        }

        public void Delete(TEntity entity)
        {
            _repository.Delete(entity);
        }

        public Task<List<TEntity>> GetAll(CancellationToken cancellationToken)
        {
            return _repository.GetAll(cancellationToken);
        }

        public Task<TEntity> GetById(int id, CancellationToken cancellationToken)
        {
            return _repository.GetById(id, cancellationToken);
        }

        public void Update(TEntity entity)
        {
            _repository.Update(entity);
        }
        public IQueryable<TEntity> Get() => _repository.Get();

    }
}
