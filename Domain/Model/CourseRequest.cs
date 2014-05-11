using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain
{
    public class CourseRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public Guid ID { get; set; }

        public virtual Student Student { get; set; }

        public Guid StudentId { get; set; }

        public virtual Course Course { get; set; }

        public Guid CourseId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public string Message { get; set; }

        public bool IsDeclined { get; set; }
    }
}
