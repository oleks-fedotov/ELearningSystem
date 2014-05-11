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
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Please write your name")]
        [MaxLength(35, ErrorMessage = "Name should be shorter than 35 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please write your surname")]
        [MaxLength(35, ErrorMessage = "Surname should be shorter than 35 characters")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please write you login name")]
        [MaxLength(35, ErrorMessage = "Login should be shorter than 35 characters")]
        [MinLength(7, ErrorMessage = "Login should be longer than 7 characters")]
        [Remote("ValidateUniqueLoginName", "Account", ErrorMessage = "This login is already used.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Please write your password")]
        [MaxLength(25, ErrorMessage = "Password should be shorter than 25 characters")]
        [MinLength(7, ErrorMessage = "Password should be longer than 7 characters")]
        public string Password { get; set; }

        public string Uni { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        public string Interests { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        public string Information { get; set; }

        [Required(ErrorMessage = "Please write your email")]
        [DataType(DataType.EmailAddress)]
        [Remote("ValidateUniqueEmail", "Account", ErrorMessage = "This email is already used.")]
        public string Email { get; set; }


    }
}
