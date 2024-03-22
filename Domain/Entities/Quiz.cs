using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Quiz : BaseEntity
    {
        public Quiz() { }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual Category? Category { get; set; }
        public int? CategoryId { get; set; }
        public List<Question> Questions { get; set; }
        
    }
}
