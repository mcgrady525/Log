using Log.Entity.Common;
using Log.Entity.ViewModel;
using Log.IService.Rights;
using Log.Site.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tracy.Frameworks.Common.Consts;
using Tracy.Frameworks.Common.Extends;
using Log.Common.Helper;

namespace Log.Site.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleController : BaseController
    {
        //注入service
        private readonly IRightsRoleService _roleService;

        public RoleController(IRightsRoleService roleService)
        {
            _roleService = roleService;
        }

        [LoginAuthorization]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 角色列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetPagingRoles(GetPagingRolesRequest request, int page, int rows)
        {
            var result = string.Empty;
            if (request == null)
            {
                request = new GetPagingRolesRequest();
            }
            request.PageIndex = page;
            request.PageSize = rows;

            var rs = _roleService.GetPagingRoles(request);
            if (rs.ReturnCode == ReturnCodeType.Success)
            {
                result = "{\"total\": " + rs.Content.TotalCount + ",\"rows\":" + rs.Content.Entities.ToJson(dateTimeFormat: DateTimeTypeConst.DATETIME) + "}";
            }

            return Content(result);
        }

        /// <summary>
        /// 获取角色下的用户列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetPagingRoleUsers(GetPagingRoleUsersRequest request, int page, int rows)
        {
            var result = string.Empty;
            if (request == null)
            {
                request = new GetPagingRoleUsersRequest();
            }
            request.PageIndex = page;
            request.PageSize = rows;

            var rs = _roleService.GetPagingRoleUsers(request);
            if (rs.ReturnCode == ReturnCodeType.Success)
            {
                result = "{\"total\": " + rs.Content.TotalCount + ",\"rows\":" + rs.Content.Entities.ToJson(dateTimeFormat: DateTimeTypeConst.DATETIME) + "}";
            }

            return Content(result);
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        [LoginAuthorization]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(AddRoleRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            if (request == null)
            {
                request = new AddRoleRequest();
            }

            var rs = _roleService.AddRole(request, loginInfo);
            if (rs.ReturnCode == ReturnCodeType.Success && rs.Content == true)
            {
                flag = true;
                msg = "新增成功!";
            }
            else
            {
                msg = rs.Message.IsNullOrEmpty() ? "新增失败!" : rs.Message;
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <returns></returns>
        [LoginAuthorization]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(EditRoleRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            var rs = _roleService.EditRole(request, loginInfo);
            if (rs.ReturnCode == ReturnCodeType.Success && rs.Content == true)
            {
                flag = true;
                msg = "修改成功!";
            }
            else
            {
                msg = rs.Message.IsNullOrEmpty() ? "修改失败!" : rs.Message;
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(DeleteRoleRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            var rs = _roleService.DeleteRole(request);
            if (rs.ReturnCode == ReturnCodeType.Success && rs.Content == true)
            {
                flag = true;
                msg = "删除成功!";
            }
            else
            {
                msg = rs.Message.IsNullOrEmpty() ? "删除失败!" : rs.Message;
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 角色授权
        /// </summary>
        /// <returns></returns>
        [LoginAuthorization]
        public ActionResult Authorize()
        {
            return View();
        }

        /// <summary>
        /// 角色授权
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Authorize(AuthorizeRoleRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            var rs = _roleService.AuthorizeRole(request);
            if (rs.ReturnCode == ReturnCodeType.Success && rs.Content == true)
            {
                flag = true;
                msg = "授权成功!";
            }
            else
            {
                msg = rs.Message.IsNullOrEmpty() ? "授权失败!" : rs.Message;
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取该角色所拥有的菜单按钮权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public ActionResult GetRoleMenuButton(int roleId)
        {
            var result = string.Empty;

            var rs = _roleService.GetRoleMenuButton(roleId);
            if (rs.ReturnCode == ReturnCodeType.Success)
            {
                var roleMenuButtons = rs.Content;
                if (roleMenuButtons.HasValue())
                {
                    result = RightsHelper.GetRoleMenuButtonStr(roleMenuButtons, roleId);
                }
            }

            return Content(result);
        }

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAll()
        {
            var result = string.Empty;

            var rs = _roleService.GetAllRole();
            if (rs.ReturnCode == ReturnCodeType.Success)
            {
                result = rs.Content.Select(p => new
                {
                    Id = p.Id,
                    RoleName = p.Name
                }).ToJson();
            }

            return Content(result);
        }

    }
}