using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Log.Common.Helper;

namespace Log.Site.Filters
{
    /// <summary>
    /// 全局异常处理
    /// </summary>
    public class GlobalHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            //记录日志
            LogHelper.Error(()=> string.Format(filterContext.Exception.ToString()));

            base.OnException(filterContext);
        }
    }
}