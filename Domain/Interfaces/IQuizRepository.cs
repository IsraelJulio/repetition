using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IQuizRepository : IBaseRepository<Quiz>
    {
        new IEnumerable<Quiz> Get();
        new Quiz Update(Quiz entity);
    }
}
