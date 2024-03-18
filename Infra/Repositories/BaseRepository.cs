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

        public void Create(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            _context.Add(entity);
            _context.SaveChanges();

        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public async Task<List<T>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task<T> GetById(int id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
        public void SaveRange(List<T> listToSave)
        {
            _context.UpdateRange(listToSave);
            _context.SaveChanges();
           
        }
        public IQueryable<T> Get() => _dbSet.AsNoTracking().AsQueryable();
    }
}
