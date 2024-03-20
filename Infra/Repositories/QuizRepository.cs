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
    public class QuizRepository : BaseRepository<Quiz>, IQuizRepository
    {
        private readonly RepetitionDbContext _dbContext;

        public QuizRepository(RepetitionDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Quiz> Get()
        {
            return base.Get().Include(x=> x.Questions);
        }
        public Quiz Update(Quiz entity)
        {
            var existingQuiz = _dbContext.Quiz.Include(x => x.Questions).FirstOrDefault(x => x.Id == entity.Id);

            if (existingQuiz != null)
            {
                _dbContext.Question.RemoveRange(existingQuiz.Questions);
                existingQuiz.Title = entity.Title;
                existingQuiz.Description = entity.Description;
                existingQuiz.Questions = entity.Questions;
                _dbContext.SaveChanges();
            }

            return existingQuiz;
        }
    }
}
