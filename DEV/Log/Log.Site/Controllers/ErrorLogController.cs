using Log.Entity.Common;
using Log.Entity.Db;
using Log.Entity.ViewModel;
using Log.IService;
using Log.Site.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using Tracy.Frameworks.Common.Extends;

namespace Log.Site.Controllers
{
    public class ErrorLogController : BaseController
    {
        //注入service
        private readonly ILogsErrorLogService _errorLogService;

        public ErrorLogController(ILogsErrorLogService errorLogService)
        {
            _errorLogService = errorLogService;
        }

        /// <summary>
        /// 列表页
        /// </summary>
        /// <returns></returns>
        [LoginAuthorization]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 详情页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(int id)
        {
            TLogsErrorLog errorLog = null;
            var rs = _errorLogService.GetErrorLogById(id);
            if (rs.ReturnCode == ReturnCodeType.Success)
            {
                errorLog = rs.Content;
            }

            return View(errorLog);
        }

        /// <summary>
        /// 获取所有错误日志(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetPagingErrorLogs(GetPagingErrorLogsRequest request, int page, int rows)
        {
            var result = string.Empty;
            if (request == null)
            {
                request = new GetPagingErrorLogsRequest();
            }
            request.PageIndex = page;
            request.PageSize = rows;

            var rs = _errorLogService.GetPagingErrorLogs(request);
            if (rs.ReturnCode == ReturnCodeType.Success)
            {
                result = "{\"total\": " + rs.Content.TotalCount + ",\"rows\":" + rs.Content.Entities.ToJson(dateTimeFormat: "yyyy-MM-dd HH:mm:ss.fff") + "}";
            }

            return Content(result);
        }

        /// <summary>
        /// 刷新智能提示
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RefreshErrorLogTip()
        {
            var flag = false;
            var msg = string.Empty;

            var rs = _errorLogService.RefreshErrorLogTip();
            if (rs.ReturnCode == ReturnCodeType.Success && rs.Content == true)
            {
                flag = true;
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取智能提示数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAutoCompleteData()
        {
            var flag = false;
            var msg = string.Empty;
            var systemCodes = new List<string>();
            var sources = new List<string>();

            var rs = _errorLogService.GetAutoCompleteData();
            if (rs.ReturnCode == ReturnCodeType.Success)
            {
                systemCodes = rs.Content.Item1;
                sources = rs.Content.Item2;
                flag = true;
            }

            return Json(new { success = flag, msg = msg, systemCodes = systemCodes.ToJson(), sources = sources.ToJson() }, JsonRequestBehavior.AllowGet);
        }

    }
}