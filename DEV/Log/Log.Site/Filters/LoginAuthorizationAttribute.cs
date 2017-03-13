using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Tracy.Frameworks.Common.Extends;
using System.ServiceModel;
using Log.Entity.Db;
using Log.Entity.Common;
using Log.IService.Rights;
using Log.Entity.ViewModel;
using Autofac;

namespace Log.Site.Filters
{
    public class LoginAuthorizationAttribute : AuthorizeAttribute
    {
        //注入service
        public IRightsAccountService AccountService { get; set; }

        /// <summary>
        /// 授权失败时呈现的视图
        /// </summary>
        public string AuthorizationFailView { get; set; }

        // 摘要: 
        //     重写时，提供一个入口点用于进行自定义授权检查。
        //
        // 参数: 
        //   httpContext:
        //     HTTP 上下文，它封装有关单个 HTTP 请求的所有 HTTP 特定的信息。
        //
        // 返回结果: 
        //     如果用户已经过授权，则为 true；否则为 false。
        //
        // 异常: 
        //   System.ArgumentNullException:
        //     httpContext 参数为 null。
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            if (!httpContext.Request.IsAuthenticated)
            {
                return false;
            }

            //已登录,需要验证cookie
            FormsIdentity id = (FormsIdentity)httpContext.User.Identity;
            FormsAuthenticationTicket oldTicket = id.Ticket;
            var userFromCookie = oldTicket.UserData.FromJson<TRightsUser>();
            var result = AccountService.CheckLogin(new CheckLoginRequest { loginId = userFromCookie.UserId, loginPwd = userFromCookie.Password });
            if (result.ReturnCode == ReturnCodeType.Success)
            {
                var userFromDB = result.Content;
                if (userFromDB == null)
                {
                    FormsAuthentication.SignOut();
                    return false;
                }
                else if (userFromDB.EnableFlag.Value == false)
                {
                    FormsAuthentication.SignOut();
                    return false;
                }
                else if (userFromCookie.IsChangePwd != userFromDB.IsChangePwd || userFromCookie.UserName != userFromDB.UserName)//校验是否修改了IfChangePwd字段或真实名
                {
                    //更新cookie
                    FormsAuthentication.SignOut();
                    FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket
                    (
                        2,
                        userFromDB.UserId,
                        DateTime.Now,
                        oldTicket.Expiration,
                        false,
                        userFromDB.ToJson()
                    );
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(newTicket));
                    if (oldTicket.Expiration != new DateTime(9999, 12, 31))
                    {
                        cookie.Expires = oldTicket.Expiration;
                    }
                    httpContext.Response.Cookies.Add(cookie);

                    return false;
                }
                else
                {
                    //...
                }
            }

            return true;
        }

        //
        // 摘要: 
        //     处理未能授权的 HTTP 请求。
        //
        // 参数: 
        //   filterContext:
        //     封装有关使用 System.Web.Mvc.AuthorizeAttribute 的信息。filterContext 对象包括控制器、HTTP 上下文、请求上下文、操作结果和路由数据。
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            //跳转到登录页(非登录页才跳转否则死循环了)
            string controllerName = filterContext.RouteData.Values["controller"].ToString().ToLower();
            string actionName = filterContext.RouteData.Values["action"].ToString().ToLower();
            if (!(controllerName.Equals("account") && actionName.Equals("login")))
            {
                var path = filterContext.HttpContext.Request.Path;
                var url = "Account/Login?ReturnUrl={0}";
                filterContext.HttpContext.Response.Redirect(string.Format(url, HttpUtility.UrlDecode(path)), true);
            }
        }
    }
}