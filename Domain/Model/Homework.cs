using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Homework
    {
        public decimal ID { get; set; }

        public virtual Student Student { get; set; }

        public double Mark { get; set; }

        public virtual Lecture Lecture { get; set; }
    }
}
