using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Answer
    {
        public Guid ID { get; set; }

        public virtual Question Question { get; set; }

        public string Text { get; set; }

        public bool IsRight { get; set; }
    }
}
