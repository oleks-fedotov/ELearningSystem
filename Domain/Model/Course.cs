using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Please write course name")]
        [MaxLength(35, ErrorMessage = "Course name should be shorter than 35 characters")]
        public string Name { get; set; }

        public Guid CategoryId { get; set; }
        
        public virtual CourseCategory Category { get; set; }

        [Range(1, 10)]
        [Required(ErrorMessage = "Please write complexity level (1-10)")]
        public decimal ComplexityLevel { get; set; }

        public Guid CourseTypeId { get; set; }

        public virtual CourseType CourseType { get; set; }

        [HiddenInput(DisplayValue = false)]
        public virtual Lecturer Lecturer { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid LecturerId { get; set; }

        [DataType(DataType.MultilineText)]
        public string RequiredSkills { get; set; }

        [HiddenInput(DisplayValue = false)]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please write a decsiption for your course")]
        [MaxLength(500)]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(2000)]
        public string CourseContent { get; set; }
    }
}
