﻿using System;
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
        public List<Question> Questions { get; set; }
        
    }
}