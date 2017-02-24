using Log.Entity.Common;
using Log.Entity.ViewModel;
using Log.IService;
using Log.Site.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using Tracy.Frameworks.Common.Consts;
using Tracy.Frameworks.Common.Extends;
using Log.Entity.Db;

namespace Log.Site.Controllers
{
    /// <summary>
    /// debug log页面
    /// </summary>
    public class DebugLogController : BaseController
    {
        /// <summary>
        /// 首页
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
        [LoginAuthorization]
        public ActionResult Detail(int id)
        {
            TLogsDebugLog debugLog = null;
            using (var factory = new ChannelFactory<ILogsDebugLogService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.GetDebugLogById(id);
                if (rs.ReturnCode == ReturnCodeType.Success)
                {
                    debugLog = rs.Content;
                }
            }

            return View(debugLog);
        }

        /// <summary>
        /// 获取所有调试日志(分页)
        /// </summary>
        /// <param name="request">查询条件</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetPagingDebugLogs(GetPagingDebugLogsRequest request, int page, int rows)
        {
            var result = string.Empty;
            if (request == null)
            {
                request = new GetPagingDebugLogsRequest();
            }
            request.PageIndex = page;
            request.PageSize = rows;

            using (var factory = new ChannelFactory<ILogsDebugLogService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.GetPagingDebugLogs(request);
                if (rs.ReturnCode == ReturnCodeType.Success)
                {
                    result = "{\"total\": " + rs.Content.TotalCount + ",\"rows\":" + rs.Content.Entities.ToJson(dateTimeFormat: "yyyy-MM-dd HH:mm:ss.fff") + "}";
                }
            }

            return Content(result);
        }

    }
}