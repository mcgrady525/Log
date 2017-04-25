using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Log.IService;
using Log.Site.Filters;
using Log.Entity.ViewModel;
using Tracy.Frameworks.Common.Consts;
using Log.Entity.Common;
using Tracy.Frameworks.Common.Extends;

namespace Log.Site.Controllers
{
    public class ErrorLogBlackListController : BaseController
    {
        //注入service
        private ILogsErrorLogBlackListService _errorLogBlackListService;

        public ErrorLogBlackListController(ILogsErrorLogBlackListService errorLogBlackListService)
        {
            _errorLogBlackListService = errorLogBlackListService;
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
        public ActionResult GetPagingBlackList(GetPagingErrorLogBlackListRequest request, int page, int rows)
        {
            var result = string.Empty;
            if (request == null)
            {
                request = new GetPagingErrorLogBlackListRequest();
            }
            request.PageIndex = page;
            request.PageSize = rows;

            var rs = _errorLogBlackListService.GetPagingBlackList(request);
            if (rs.ReturnCode == ReturnCodeType.Success)
            {
                result = "{\"total\": " + rs.Content.TotalCount + ",\"rows\":" + rs.Content.Entities.ToJson(dateTimeFormat: DateTimeTypeConst.DATETIME) + "}";
            }

            return Content(result);
        }

        /// <summary>
        /// 添加黑名单
        /// </summary>
        /// <returns></returns>
        [LoginAuthorization]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 添加黑名单(post)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(InsertErrorLogBlackListRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            if (request == null)
            {
                request = new InsertErrorLogBlackListRequest();
            }

            //校验
            if (request.KeyWord.IsNullOrEmpty())
            {
                msg = "关键字不能为空!";
                return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
            }

            var rs = _errorLogBlackListService.Insert(request, loginInfo);
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
        /// 删除黑名单(post)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(DeleteErrorLogBlackListRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            if (request == null)
            {
                request = new DeleteErrorLogBlackListRequest();
            }

            var rs = _errorLogBlackListService.DeleteBlackList(request);
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