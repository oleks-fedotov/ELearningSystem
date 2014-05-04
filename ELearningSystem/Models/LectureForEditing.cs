using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ELearningSystem.Models
{
    public class LectureForEditing
    {
        public Guid ID { get; set; }

        public Guid TopicId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Homework { get; set; }

        [Required]
        public string LectureContent { get; set; }

        public decimal OrderNumber { get; set; }

        public List<string> Files { get; set; }
    }
}