using Log.Entity.Common;
using Log.IService.Rights;
using Log.Site.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tracy.Frameworks.Common.Extends;
using Tracy.Frameworks.Common.Consts;
using Log.Entity.Db;
using Log.Entity.ViewModel;

namespace Log.Site.Controllers
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    public class MenuController : BaseController
    {
        [LoginAuthorization]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取所有菜单，以树形展示
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAll()
        {
            var result = string.Empty;
            StringBuilder sb = new StringBuilder();

            using (var factory = new ChannelFactory<IRightsMenuService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.GetAll();
                if (rs.ReturnCode == ReturnCodeType.Success)
                {
                    var menus = rs.Content;
                    if (menus.HasValue())
                    {
                        sb.Append(RecursionMenu(menus, 0));
                        sb = sb.Remove(sb.Length - 2, 2);
                        result = sb.ToString();
                    }
                    else
                    {
                        result = "[]";
                    }
                }
            }

            return Content(result);
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <returns></returns>
        [LoginAuthorization]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(AddMenuRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            using (var factory = new ChannelFactory<IRightsMenuService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.AddMenu(request, loginInfo);
                if (rs.ReturnCode == ReturnCodeType.Success && rs.Content == true)
                {
                    flag = true;
                    msg = "新增成功!";
                }
                else
                {
                    msg = "新增失败!";
                }
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <returns></returns>
        [LoginAuthorization]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(EditMenuRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            using (var factory = new ChannelFactory<IRightsMenuService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.EditMenu(request, loginInfo);
                if (rs.ReturnCode == ReturnCodeType.Success && rs.Content == true)
                {
                    flag = true;
                    msg = "修改成功!";
                }
                else
                {
                    msg = "修改失败!";
                }
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(DeleteMenuRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            using (var factory = new ChannelFactory<IRightsMenuService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.DeleteMenu(request);
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
        /// 分配按钮
        /// </summary>
        /// <returns></returns>
        [LoginAuthorization]
        public ActionResult SetButton()
        {
            return View();
        }

        /// <summary>
        /// 分配按钮
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetButton(SetButtonRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            using (var factory = new ChannelFactory<IRightsMenuService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.SetButton(request);
                if (rs.ReturnCode == ReturnCodeType.Success && rs.Content == true)
                {
                    flag = true;
                    msg = "分配按钮成功!";
                }
                else
                {
                    msg = "分配按钮失败!";
                }
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取当前菜单关联的按钮列表
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetButton(string menuId)
        {
            var result = string.Empty;
            using (var factory = new ChannelFactory<IRightsMenuService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.GetButton(menuId);
                if (rs.ReturnCode == ReturnCodeType.Success)
                {
                    result = rs.Content.ToJson();
                }
            }

            return Content(result);
        }


        #region Private method
        private string RecursionMenu(List<TRightsMenu> list, int parentId)
        {
            StringBuilder sb = new StringBuilder();
            var childMenus = list.Where(p => p.ParentId == parentId).ToList();
            if (childMenus.HasValue())
            {
                sb.Append("[");
                for (int i = 0; i < childMenus.Count; i++)
                {
                    var childStr = RecursionMenu(list, childMenus[i].Id);
                    var lastUpdatedTime = childMenus[i].LastUpdatedTime.HasValue ? childMenus[i].LastUpdatedTime.Value.ToString(DateTimeTypeConst.DATETIME) : "";
                    if (!childStr.IsNullOrEmpty())
                    {
                        sb.Append("{\"id\":\"" + childMenus[i].Id.ToString() + "\",\"Code\":\"" + childMenus[i].Code + "\",\"Url\":\"" + childMenus[i].Url + "\",\"Icon\":\"" + childMenus[i].Icon + "\",\"Sort\":\"" + childMenus[i].Sort.Value + "\",\"CreatedTime\":\"" + childMenus[i].CreatedTime.ToString(DateTimeTypeConst.DATETIME) + "\",\"LastUpdatedTime\":\"" + lastUpdatedTime + "\",\"ParentId\":\"" + childMenus[i].ParentId.ToString() + "\",\"text\":\"" + childMenus[i].Name + "\",\"children\":");
                        sb.Append(childStr);
                    }
                    else
                    {
                        sb.Append("{\"id\":\"" + childMenus[i].Id.ToString() + "\",\"Code\":\"" + childMenus[i].Code + "\",\"Url\":\"" + childMenus[i].Url + "\",\"Icon\":\"" + childMenus[i].Icon + "\",\"Sort\":\"" + childMenus[i].Sort.Value + "\",\"CreatedTime\":\"" + childMenus[i].CreatedTime.ToString(DateTimeTypeConst.DATETIME) + "\",\"LastUpdatedTime\":\"" + lastUpdatedTime + "\",\"ParentId\":\"" + childMenus[i].ParentId.ToString() + "\",\"text\":\"" + childMenus[i].Name + "\"},");
                    }

                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("]},");
            }
            return sb.ToString();
        }
        #endregion

    }
}