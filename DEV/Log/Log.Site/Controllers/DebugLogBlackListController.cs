using Log.Site.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Log.IService;
using Log.Entity.Common;
using Tracy.Frameworks.Common.Consts;
using Log.Entity.ViewModel;
using Tracy.Frameworks.Common.Extends;

namespace Log.Site.Controllers
{
    public class DebugLogBlackListController : BaseController
    {
        //注入service
        private ILogsDebugLogBlackListService _debugLogBlackListService;

        public DebugLogBlackListController(ILogsDebugLogBlackListService debugLogBlackListService)
        {
            _debugLogBlackListService = debugLogBlackListService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [LoginAuthorization]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取黑名单列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetPagingBlackList(GetPagingDebugLogBlackListRequest request, int page, int rows)
        {
            var result = string.Empty;
            if (request == null)
            {
                request = new GetPagingDebugLogBlackListRequest();
            }
            request.PageIndex = page;
            request.PageSize = rows;

            var rs = _debugLogBlackListService.GetPagingBlackList(request);
            if (rs.ReturnCode == ReturnCodeType.Success)
            {
                result = "{\"total\": " + rs.Content.TotalCount + ",\"rows\":" + rs.Content.Entities.ToJson(dateTimeFormat: DateTimeTypeConst.DATETIME) + "}";
            }

            return Content(result);
        }

        /// <summary>
        /// 增加黑名单
        /// </summary>
        /// <returns></returns>
        [LoginAuthorization]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 增加黑名单(post)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(InsertDebugLogBlackListRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            if (request == null)
            {
                request = new InsertDebugLogBlackListRequest();
            }

            //校验
            if (request.KeyWord.IsNullOrEmpty())
            {
                msg = "关键字不能为空!";
                return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
            }

            var rs = _debugLogBlackListService.Insert(request, loginInfo);
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
        /// 删除黑名单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(DeleteDebugLogBlackListRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            if (request == null)
            {
                request = new DeleteDebugLogBlackListRequest();
            }

            var rs = _debugLogBlackListService.Delete(request);
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

    }
}