using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class StudentCourse
    {
        public Guid ID { get; set; }

        public virtual Student Student { get; set; }

        public virtual Course Course { get; set; }

        public double Mark { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
