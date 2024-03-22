using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly RepetitionDbContext _dbContext;

        public CategoryRepository(RepetitionDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //new Category Update(Category entity)
        //{

        //    _dbContext.Category.Where(old => old.Name == entity.Name).FirstOrDefault();
        //    if (entity != null) { throw new ArgumentException("This Category Name alred exists"); }
        //    else return base.Update(entity);
        //}
        //private bool ValidateName
    }
}
