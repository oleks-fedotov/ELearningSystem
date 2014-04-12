using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELearningSystem.Models
{
    public class UserInformation
    {
        public Guid? UserId { get; set; }

        public string UserName { get; set; }

        public bool IsStudent { get; set; }
    }
}