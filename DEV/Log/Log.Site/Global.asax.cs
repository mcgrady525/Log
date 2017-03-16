using Log.Site.Filters;
using Log.Site.Helpers;
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

            //Autofac初始化
            AutofacHelper.Init();

            //未处理异常
            GlobalFilters.Filters.Add(new GlobalHandleErrorAttribute());

        }

        protected void Application_BeginRequest(Object source, EventArgs e)
        {
            //开启MiniProfiler
            //MiniProfiler.Start();
        }

        protected void Application_EndRequest()
        {
            //停止MiniProfiler
            //MiniProfiler.Stop();
        }
    }
}
