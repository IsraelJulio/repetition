using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(TEntity entity);
        List<TEntity> SaveRange(List<TEntity> list);
        Task <TEntity> GetById(int id, CancellationToken cancellationToken);
        Task <List<TEntity>> GetAll(CancellationToken cancellationToken);
        IQueryable<TEntity> Get();
        void Save();
    }
}
