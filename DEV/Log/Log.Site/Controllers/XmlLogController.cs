﻿using Log.Entity.Common;
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
    /// <summary>
    /// xml日志
    /// </summary>
    public class XmlLogController : BaseController
    {
        //注入service
        private readonly ILogsXmlLogService _xmlLogService;

        public XmlLogController(ILogsXmlLogService xmlLogService)
        {
            _xmlLogService = xmlLogService;
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
        [LoginAuthorization]
        public ActionResult Detail(long id)
        {
            TLogsXmlLog xmlLog = null;
            var rs = _xmlLogService.GetXmlLogById(id);
            if (rs.ReturnCode == ReturnCodeType.Success)
            {
                xmlLog = rs.Content;
            }

            return View(xmlLog);
        }

        /// <summary>
        /// 获取所有日志(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetPagingXmlLogs(GetPagingXmlLogsRequest request, int page, int rows)
        {
            var result = string.Empty;
            if (request == null)
            {
                request = new GetPagingXmlLogsRequest();
            }
            request.PageIndex = page;
            request.PageSize = rows;

            var rs = _xmlLogService.GetPagingXmlLogs(request);
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
        public ActionResult RefreshXmlLogTip()
        {
            var flag = false;
            var msg = string.Empty;

            var rs = _xmlLogService.RefreshXmlLogTip();
            if (rs.ReturnCode == ReturnCodeType.Success && rs.Content == true)
            {
                flag = true;
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取智能提示数据源
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAutoCompleteData()
        {
            var flag = false;
            var msg = string.Empty;
            var systemCodes = new List<string>();
            var sources = new List<string>();
            var classNames = new List<string>();
            var methodNames = new List<string>();

            var rs = _xmlLogService.GetAutoCompleteData();
            if (rs.ReturnCode == ReturnCodeType.Success)
            {
                systemCodes = rs.Content.Item1;
                sources = rs.Content.Item2;
                classNames = rs.Content.Item3;
                methodNames = rs.Content.Item4;
                flag = true;
            }

            return Json(new { success = flag, msg = msg, systemCodes = systemCodes.ToJson(), sources = sources.ToJson(), classNames = classNames.ToJson(), methodNames = methodNames.ToJson() }, JsonRequestBehavior.AllowGet);
        }

    }
}