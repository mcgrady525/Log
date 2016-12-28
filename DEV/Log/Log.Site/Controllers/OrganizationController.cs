using Log.Site.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Log.IService.Rights;
using Log.Entity.Common;
using Log.Entity.Db;
using Tracy.Frameworks.Common.Extends;
using Tracy.Frameworks.Common.Consts;
using Log.Common.Helper;
using Log.Entity.ViewModel;

namespace Log.Site.Controllers
{
    /// <summary>
    /// 组织机构
    /// </summary>
    public class OrganizationController : BaseController
    {
        [LoginAuthorization]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取组织机构树，返回json数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetTreeOrgData()
        {
            //先获取所有机构
            //然后递归生成JSON数据
            var result = string.Empty;
            StringBuilder sb = new StringBuilder();

            using (var factory = new ChannelFactory<IRightsOrganizationService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.GetAll();
                if (rs.ReturnCode == ReturnCodeType.Success)
                {
                    var orgs = rs.Content;
                    if (orgs.HasValue())
                    {
                        sb.Append(RecursionOrg(orgs, 0));
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
        /// 获取当前用户当前页面可以访问的按钮列表
        /// </summary>
        /// <param name="menuCode">联表查询时用到</param>
        /// <param name="pageName">生成按钮的事件时用到</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetButtonsByUserIdAndMenuCode(string menuCode, string pageName)
        {
            if (menuCode.IsNullOrEmpty())
            {
                throw new ArgumentNullException("menuCode");
            }
            if (pageName.IsNullOrEmpty())
            {
                throw new ArgumentNullException("pageName");
            }

            var result = string.Empty;
            using (var factory = new ChannelFactory<IRightsOrganizationService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.GetButtonsByUserIdAndMenuCode(menuCode, loginInfo.Id);
                if (rs.ReturnCode == ReturnCodeType.Success)
                {
                    var dt = rs.Content.ToDataTable();
                    result = ToolbarHelper.GetToolBar(dt, pageName);
                }
            }

            return Content(result);
        }

        /// <summary>
        /// 获取指定机构的所有子机构，0表示获取所有，包含当前选择的机构
        /// </summary>
        /// <param name="orgId">机构id</param>
        /// <returns></returns>
        public ActionResult GetChildrenOrgs(int orgId)
        {
            //先获取指定机构的所有子机构
            //然后递归生成JSON数据
            var result = string.Empty;

            using (var factory = new ChannelFactory<IRightsOrganizationService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.GetChildrenOrgs(orgId);
                if (rs.ReturnCode == ReturnCodeType.Success)
                {
                    var orgs = rs.Content;
                    if (orgs.HasValue())
                    {
                        result = CreateChildrenOrgStr(orgs, orgId);
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
        /// 新增机构
        /// </summary>
        /// <returns></returns>
        [LoginAuthorization]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddOrganizationRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            using (var factory = new ChannelFactory<IRightsOrganizationService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.AddOrganization(request, loginInfo);
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

        [LoginAuthorization]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(EditOrganizationRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            using (var factory = new ChannelFactory<IRightsOrganizationService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.EditOrganization(request, loginInfo);
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

        /// <summary>
        /// 删除组织机构(支持批量删除)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(DeleteOrganizationRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            using (var factory = new ChannelFactory<IRightsOrganizationService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.DeleteOrganization(request);
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

        #region Private method

        /// <summary>
        /// 获取指定机构的所有子机构json(包括当前机构)
        /// </summary>
        /// <param name="list"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        private string CreateChildrenOrgStr(List<TRightsOrganization> list, int orgId)
        {
            StringBuilder sb = new StringBuilder();
            if (orgId == 0)
            {
                sb.Append(RecursionOrg(list, orgId));
                sb = sb.Remove(sb.Length - 2, 2);
            }
            else
            {
                var currentOrg = list.First(p => p.Id == orgId);
                var createdTime = currentOrg.CreatedTime.ToString(DateTimeTypeConst.DATETIME);
                var lastUpdatedTime = currentOrg.LastUpdatedTime.HasValue ? currentOrg.LastUpdatedTime.Value.ToString(DateTimeTypeConst.DATETIME) : "";

                sb.Append("[{");
                sb.Append("\"id\":\"" + currentOrg.Id.ToString() + "\",\"Code\":\"" + currentOrg.Code + "\",\"Enabled\":\"" + currentOrg.EnableFlag.Value + "\",\"Sort\":\"" + currentOrg.Sort.Value + "\",\"CreatedTime\":\"" + createdTime + "\",\"LastUpdatedTime\":\"" + lastUpdatedTime + "\",\"ParentId\":\"" + currentOrg.ParentId.ToString() + "\",\"text\":\"" + currentOrg.Name + "\"");

                var childOrgs = list.Where(p => p.ParentId == orgId).ToList();
                if (childOrgs.HasValue())
                {
                    sb.Append(",\"children\":");
                    sb.Append(RecursionOrg(list, orgId));
                    sb = sb.Remove(sb.Length - 2, 2);
                }
                sb.Append("}]");
            }

            return sb.ToString();
        }

        private string RecursionOrg(List<TRightsOrganization> list, int parentId)
        {
            StringBuilder sb = new StringBuilder();
            var childOrgs = list.Where(p => p.ParentId == parentId).ToList();
            if (childOrgs.HasValue())
            {
                sb.Append("[");
                for (int i = 0; i < childOrgs.Count; i++)
                {
                    var childStr = RecursionOrg(list, childOrgs[i].Id);
                    var createdTime = childOrgs[i].CreatedTime.ToString(DateTimeTypeConst.DATETIME);
                    var lastUpdatedTime = childOrgs[i].LastUpdatedTime.HasValue ? childOrgs[i].LastUpdatedTime.Value.ToString(DateTimeTypeConst.DATETIME) : "";

                    if (!childStr.IsNullOrEmpty())
                    {
                        sb.Append("{\"id\":\"" + childOrgs[i].Id.ToString() + "\",\"Code\":\"" + childOrgs[i].Code + "\",\"Enabled\":\"" + childOrgs[i].EnableFlag.Value + "\",\"Sort\":\"" + childOrgs[i].Sort.Value + "\",\"CreatedTime\":\"" + createdTime + "\",\"LastUpdatedTime\":\"" + lastUpdatedTime + "\",\"ParentId\":\"" + childOrgs[i].ParentId.ToString() + "\",\"text\":\"" + childOrgs[i].Name + "\",\"children\":");
                        sb.Append(childStr);
                    }
                    else
                    {
                        sb.Append("{\"id\":\"" + childOrgs[i].Id.ToString() + "\",\"Code\":\"" + childOrgs[i].Code + "\",\"Enabled\":\"" + childOrgs[i].EnableFlag.Value + "\",\"Sort\":\"" + childOrgs[i].Sort.Value + "\",\"CreatedTime\":\"" + createdTime + "\",\"LastUpdatedTime\":\"" + lastUpdatedTime + "\",\"ParentId\":\"" + childOrgs[i].ParentId.ToString() + "\",\"text\":\"" + childOrgs[i].Name + "\"},");
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