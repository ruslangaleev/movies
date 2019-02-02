using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;
using Unity.Lifetime;
using Unity.RegistrationByConvention;

namespace Movies.Api.Models
{
    public class UnityJobActivator : JobActivator
    {
        private readonly IUnityContainer hangfireContainer;

        public UnityJobActivator(IUnityContainer hangfireContainer)
        {
            this.hangfireContainer = hangfireContainer;
            //this.hangfireContainer
            //       .RegisterTypes(new PerResolveLifetimeManager());
            //don't forget to register child dependencies as well
        }


        public override object ActivateJob(Type type)
        {
            return hangfireContainer.Resolve(type);
        }
    }
}