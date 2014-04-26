using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TestResult
    {
        public decimal ID { get; set; }

        public virtual Student Student { get; set; }

        public virtual Test Test { get; set; }

        public DateTime Date { get; set; }

        public double Mark { get; set; }
    }
}
