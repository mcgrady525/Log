using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac.Integration.WebApi;
using Autofac.Integration.Mvc;
using System.Web.Mvc;

namespace Log.OpenApi.Helpers
{
    public class AutofacHelper
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            var builder = new ContainerBuilder();
            HttpConfiguration config = GlobalConfiguration.Configuration;

            //Controller注册
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var iService = Assembly.Load("Tracy.Frameworks.RabbitMQ");
            builder.RegisterAssemblyTypes(iService).Where(t => t.Name.EndsWith("Wrapper")).AsImplementedInterfaces();

            //Filter注册
            //builder.RegisterFilterProvider();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}