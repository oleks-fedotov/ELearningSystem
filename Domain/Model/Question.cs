using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Question
    {
        public decimal ID { get; set; }

        public virtual Test Test { get; set; }

        public int? QuestionNumber { get; set; }

        public string Text { get; set; }
    }
}
