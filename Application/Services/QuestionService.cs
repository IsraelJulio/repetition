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
    public class QuestionService : BaseService<Question>, IQuestionService
    {
        private readonly IQuestionRepository _repo;
        public QuestionService(IQuestionRepository repo) : base(repo)
        {
            _repo = repo;
        }
    }
}
