using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;



namespace Infra.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly RepetitionDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(RepetitionDbContext context)
        {
            _context = context;
            this._dbSet = _context.Set<T>();
        }

        public virtual T Create(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual T Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task<List<T>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task<T> GetById(int id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public virtual T Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
            return entity;
        }
        public virtual List<T> SaveRange(List<T> listToSave)
        {
            _dbSet.UpdateRange(listToSave);
            _context.SaveChanges();
            return listToSave;
           
        }
        public IQueryable<T> Get() => _dbSet.AsNoTracking().AsQueryable();

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
