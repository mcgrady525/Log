using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Log.Site.Helpers
{
    public class AutofacHelper
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            var builder = new ContainerBuilder();

            //Controller注册
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            var iDao = Assembly.Load("Log.IDao");
            var dao = Assembly.Load("Log.Dao");
            var iService = Assembly.Load("Log.IService");
            var service = Assembly.Load("Log.Service");
            builder.RegisterAssemblyTypes(iDao, dao).Where(t => t.Name.EndsWith("Dao")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(iService, service).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();

            //Filter注册
            builder.RegisterFilterProvider();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

    }
}