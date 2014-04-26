using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class HomeworkContent
    {
        public decimal ID { get; set; }

        public virtual Homework Homework { get; set; }

        public string FileName { get; set; }
    }
}
