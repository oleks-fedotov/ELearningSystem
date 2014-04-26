using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LectureContent
    {
        public decimal ID { get; set; }

        public virtual Lecture Lecture { get; set; }

        public int OrderNumber { get; set; }

        public string FileName { get; set; }
    }
}
