using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Lecture
    {
        public Guid ID { get; set; }

        public Guid TopicId { get; set; }

        public virtual CourseTopic Topic { get; set; }

        public string Name { get; set; }

        public string Homework { get; set; }

        public string LectureContent { get; set; }
    }
}
