using Autofac;
using Autofac.Integration.Mvc;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Log.Site
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //注册路由
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //注册静态资源
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Autofac初始化
            var builder = new ContainerBuilder();

            //Controller注册(通过构造函数)
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyTypes(typeof(Log.Dao.LogsDebugLogDao).Assembly).Where(t => t.Name.EndsWith("Dao")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(Log.Service.LogsDebugLogService).Assembly).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();

            //Filter注册(通过属性)
            builder.RegisterType<Log.Service.Rights.RightsAccountService>().As<Log.IService.Rights.IRightsAccountService>();
            builder.RegisterFilterProvider();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //其它...

        }

        protected void Application_BeginRequest(Object source, EventArgs e)
        {
            //开启MiniProfiler
            MiniProfiler.Start();
        }

        protected void Application_EndRequest()
        {
            //停止MiniProfiler
            MiniProfiler.Stop();
        }
    }
}
