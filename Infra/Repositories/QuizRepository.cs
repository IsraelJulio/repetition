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
            return base.Get().Include(x=> x.Questions).Include(c=> c.Category);
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
                existingQuiz.Questions.ForEach(q => q.Rate = q.RightQuestions > 0 ? ((q.RightQuestions + q.WrongQuestions) / q.RightQuestions) * 100 : 0);
                _dbContext.SaveChanges();
            }

            return existingQuiz;
        }
    }
}
