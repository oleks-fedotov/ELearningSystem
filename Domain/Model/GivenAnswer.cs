using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class GivenAnswer
    {
        public decimal ID { get; set; }

        public virtual QuestionResult QuestionResult { get; set; }

        public string AnswerText { get; set; }

        public bool IsSelected { get; set; }

        public bool IsRight { get; set; }
    }
}
