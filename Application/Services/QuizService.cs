using Application.Interfaces;
using Application.Services.Generic;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class QuizService : BaseService<Quiz>, IQuizService
    {
        private readonly IQuizRepository _repo;
        public QuizService(IQuizRepository repo) : base(repo)
        {
            _repo = repo;
        }
    }
}
