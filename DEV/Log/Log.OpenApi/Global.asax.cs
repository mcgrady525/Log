using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Log.Common.Helper;
using Log.OpenApi.Helpers;

namespace Log.OpenApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //autofac初始化
            AutofacHelper.Init();
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
