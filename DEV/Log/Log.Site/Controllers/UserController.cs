using Log.Entity.Common;
using Log.Entity.ViewModel;
using Log.IService.Rights;
using Log.Site.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using Tracy.Frameworks.Common.Consts;
using Tracy.Frameworks.Common.Extends;

namespace Log.Site.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserController : BaseController
    {
        [LoginAuthorization]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取指定机构(包括所有子机构)下的所有用户，默认为0表示所有机构
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public ActionResult GetPagingUsers(GetPagingUsersRequest request, int page, int rows)
        {
            var result = string.Empty;
            if (request == null)
            {
                request = new GetPagingUsersRequest();
            }
            request.PageIndex = page;
            request.PageSize = rows;

            using (var factory = new ChannelFactory<IRightsUserService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.GetPagingUsers(request);
                if (rs.ReturnCode == ReturnCodeType.Success)
                {
                    result = "{\"total\": " + rs.Content.TotalCount + ",\"rows\":" + rs.Content.Entities.ToJson(dateTimeFormat: DateTimeTypeConst.DATETIME) + "}";
                }
            }

            return Content(result);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <returns></returns>
        [LoginAuthorization]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddUserRequest request, string enableFlag, string isChangePwd)
        {
            var flag = false;
            var msg = string.Empty;

            if (request == null)
            {
                request = new AddUserRequest();
            }
            request.EnableFlag = !enableFlag.IsNullOrEmpty() ? true : false;
            request.IsChangePwd = !isChangePwd.IsNullOrEmpty() ? true : false;

            using (var factory = new ChannelFactory<IRightsUserService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.AddUser(request, loginInfo);
                if (rs.ReturnCode == ReturnCodeType.Success && rs.Content == true)
                {
                    flag = true;
                    msg = "新增成功!";
                }
                else
                {
                    msg = rs.Message;
                }
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        [LoginAuthorization]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(EditUserRequest request, string enableFlag, string isChangePwd)
        {
            var flag = false;
            var msg = string.Empty;

            if (request == null)
            {
                request = new EditUserRequest();
            }
            request.EnableFlag = !enableFlag.IsNullOrEmpty() ? true : false;
            request.IsChangePwd = !isChangePwd.IsNullOrEmpty() ? true : false;

            using (var factory = new ChannelFactory<IRightsUserService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.EditUser(request, loginInfo);
                if (rs.ReturnCode == ReturnCodeType.Success && rs.Content == true)
                {
                    flag = true;
                    msg = "修改成功!";
                }
                else
                {
                    msg = rs.Message;
                }
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(DeleteUserRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            using (var factory = new ChannelFactory<IRightsUserService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.DeleteUser(request);
                if (rs.ReturnCode == ReturnCodeType.Success && rs.Content == true)
                {
                    flag = true;
                    msg = "删除成功!";
                }
                else
                {
                    msg = "删除失败!";
                }
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 设置机构
        /// </summary>
        /// <returns></returns>
        [LoginAuthorization]
        public ActionResult SetOrg()
        {
            return View();
        }

        /// <summary>
        /// 设置机构
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetOrg(SetOrgRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            if (request.OrgIds.IsNullOrEmpty())
            {
                msg = "请选择机构!";
                return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
            }

            using (var factory = new ChannelFactory<IRightsUserService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.SetOrg(request);
                if (rs.ReturnCode == ReturnCodeType.Success && rs.Content == true)
                {
                    flag = true;
                    msg = "设置机构成功!";
                }
                else
                {
                    msg = rs.Message.IsNullOrEmpty() ? "设置机构失败!" : rs.Message;
                }
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 设置角色
        /// </summary>
        /// <returns></returns>
        [LoginAuthorization]
        public ActionResult SetRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SetRole(SetRoleRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            if (request.RoleIds.IsNullOrEmpty())
            {
                msg = "请选择角色!";
                return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
            }

            using (var factory = new ChannelFactory<IRightsUserService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.SetRole(request);
                if (rs.ReturnCode == ReturnCodeType.Success && rs.Content == true)
                {
                    flag = true;
                    msg = "设置角色成功!";
                }
                else
                {
                    msg = rs.Message.IsNullOrEmpty() ? "设置角色失败!" : rs.Message;
                }
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

    }
}