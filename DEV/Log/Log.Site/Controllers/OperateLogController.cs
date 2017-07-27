using Log.Site.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Log.IService;
using Log.Entity.Common;
using Log.Entity.ViewModel;
using Tracy.Frameworks.Common.Extends;
using Log.Entity.Db;

namespace Log.Site.Controllers
{
    /// <summary>
    /// operate log操作日志
    /// </summary>
    public class OperateLogController : BaseController
    {
        //注入service
        private readonly ILogsOperateLogService _operateLogService;

        public OperateLogController(ILogsOperateLogService operateLogService)
        {
            _operateLogService = operateLogService;
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
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(long id)
        {
            TLogsOperateLog operateLog = null;
            var rs = _operateLogService.GetById(id);
            if (rs.ReturnCode == ReturnCodeType.Success)
            {
                operateLog = rs.Content;
            }

            return View(operateLog);
        }

        /// <summary>
        /// 获取所有操作日志(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetPagingOperateLogs(GetPagingOperateLogsRequest request, int page, int rows)
        {
            var result = string.Empty;
            if (request == null)
            {
                request = new GetPagingOperateLogsRequest();
            }
            request.PageIndex = page;
            request.PageSize = rows;

            var rs = _operateLogService.GetPagingOperateLogs(request);
            if (rs.ReturnCode == ReturnCodeType.Success)
            {
                result = "{\"total\": " + rs.Content.TotalCount + ",\"rows\":" + rs.Content.Entities.ToJson(dateTimeFormat: "yyyy-MM-dd HH:mm:ss.fff") + "}";
            }

            return Content(result);
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
            var operateModules = new List<string>();
            var operateTypes = new List<string>();

            var rs = _operateLogService.GetAutoCompleteData();
            if (rs.ReturnCode == ReturnCodeType.Success)
            {
                systemCodes = rs.Content.Item1;
                sources = rs.Content.Item2;
                operateModules = rs.Content.Item3;
                operateTypes = rs.Content.Item4;
                flag = true;
            }

            return Json(new { success = flag, msg = msg, systemCodes = systemCodes.ToJson(), sources = sources.ToJson(), operateModules = operateModules.ToJson(), operateTypes = operateTypes.ToJson() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 刷新智能提示
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RefreshOperateLogTip()
        {
            var flag = false;
            var msg = string.Empty;

            var rs = _operateLogService.RefreshOperateLogTip();
            if (rs.ReturnCode == ReturnCodeType.Success && rs.Content == true)
            {
                flag = true;
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

    }
}