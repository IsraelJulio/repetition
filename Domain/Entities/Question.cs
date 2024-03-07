using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Question : BaseEntity
    {
        public Question() { }
        public string Front { get; set; }
        public string Back { get; set; }
        public long Rate { get; set; }
        public Quiz Quiz { get; set; }
        public int? QuizId { get; set; }
    }
}
