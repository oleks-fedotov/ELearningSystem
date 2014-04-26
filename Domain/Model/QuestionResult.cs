using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class QuestionResult
    {
        public decimal ID { get; set; }

        public virtual TestResult TestResult { get; set; }

        public string QuestionText { get; set; }

        public int QuestionNumber { get; set; }
    }
}
