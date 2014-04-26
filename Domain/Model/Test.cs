using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Test
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public Guid TopicId { get; set; }

        public virtual CourseTopic Topic { get; set; }

        public int Duration { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
