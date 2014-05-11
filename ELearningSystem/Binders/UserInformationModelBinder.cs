using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ELearningSystem.Models;

namespace ELearningSystem.Binders
{
    public class UserInformationModelBinder : IModelBinder
    {
        private const string userNameSessionKey = "username";
        private const string userIdSessionKey = "userid";
        private const string userIsStudentKey = "isStudent";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            UserInformation info = new UserInformation();
            info.UserName = controllerContext.HttpContext.Session[userNameSessionKey] as string;
            info.UserId = controllerContext.HttpContext.Session[userIdSessionKey] as Guid?;
            info.IsStudent = (controllerContext.HttpContext.Session[userIsStudentKey] as bool?) ?? false;
            if (info.UserId == null || info.UserName == null)
                return null;
            return info;
        }
    }
}