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
        public QuizRepository(RepetitionDbContext context): base(context) { }

        public IEnumerable<Quiz> Get()
        {
            return base.Get().Include(x=> x.Questions);
        }
    }
}
