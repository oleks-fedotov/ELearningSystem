using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CourseRequest
    {
        public Guid ID { get; set; }

        public virtual Student Student { get; set; }

        public Guid StudentId { get; set; }

        public virtual Course Course { get; set; }

        public Guid CourseId { get; set; }

        public DateTime Date { get; set; }

        public string Message { get; set; }
    }
}
