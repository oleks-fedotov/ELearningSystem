﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using Domain.Concrete;
using Domain.Abstract;
using System.Web.Security;

namespace ELearningSystem.Infrastructure
{
    public class NinjectControllerFactory: DefaultControllerFactory
    {
        private IKernel _ninjectKernel;

        public NinjectControllerFactory()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_ninjectKernel.Get(controllerType);           
        }

        private void AddBindings()
        {
            _ninjectKernel.Bind<IElearningSystemRepository>().To<EFElearningSystemRepository>();
            _ninjectKernel.Bind<MembershipProvider>().To<CustomMembershipProvider>();
        }
    }
}