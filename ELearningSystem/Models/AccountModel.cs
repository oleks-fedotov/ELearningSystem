using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ELearningSystem.Models
{
    public class AccountModel
    {
        [Required(ErrorMessage="Please write your login name")]
        [MinLength(7, ErrorMessage="Login should be longer than 7 characters")]
        [MaxLength(25, ErrorMessage = "Login should be shorter than 25 character")]
        public string UserName { get; set; }

        [Required(ErrorMessage="Please write your password")]
        [MinLength(7, ErrorMessage = "Password should be longer than 7 characters")]
        [MaxLength(25, ErrorMessage = "Password should be shorter than 25 character")]
        public string Password { get; set; }
    }
}