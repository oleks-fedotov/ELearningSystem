using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELearningSystem.Models
{
    public class StudentEditProfileModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please write your name")]
        [MaxLength(35, ErrorMessage = "Name should be shorter than 35 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please write your surname")]
        [MaxLength(35, ErrorMessage = "Surname should be shorter than 35 characters")]
        public string Surname { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        public string Information { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        public string Interests { get; set; }
    }

    public class LecturerEditProfileModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [MaxLength(35, ErrorMessage = "Name should be shorter than 35 characters")]
        public string Name { get; set; }

        [MaxLength(35, ErrorMessage = "Surname should be shorter than 35 characters")]
        public string Surname { get; set; }

        public bool IsAcademic { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        public string Interests { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        public string Information { get; set; }
    }
}