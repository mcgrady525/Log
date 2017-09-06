using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Log.Entity.Db;
using System.Web.Security;
using Tracy.Frameworks.Common.Extends;

namespace Log.Site.Controllers
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 保存当前登录用户会话
        /// </summary>
        public TRightsUser loginInfo { get; set; }

        /// <summary>
        /// Action执行前调用
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                FormsIdentity id = (FormsIdentity)filterContext.HttpContext.User.Identity;
                FormsAuthenticationTicket tickets = id.Ticket;

                loginInfo = tickets.UserData.FromJson<TRightsUser>();
            }
        }
	}
}