using Log.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace Log.OpenApi.Filters
{
    /// <summary>
    /// WebApi全局异常处理
    /// </summary>
    public class GlobalHandleErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            //记录日志
            LogHelper.Error(() => string.Format(filterContext.Exception.ToString()));

            base.OnException(filterContext);
        }

    }
}