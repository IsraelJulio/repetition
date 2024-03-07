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
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void SaveRange(List<TEntity> list);
        Task <TEntity> GetById(int id, CancellationToken cancellationToken);
        Task <List<TEntity>> GetAll(CancellationToken cancellationToken);
        IQueryable<TEntity> Get();
    }
}
