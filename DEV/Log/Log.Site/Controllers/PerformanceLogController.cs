using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Log.Site.Controllers
{
    /// <summary>
    /// 性能日志
    /// </summary>
    public class PerformanceLogController : BaseController
    {
        //
        // GET: /PerformanceLog/
        public ActionResult Index()
        {
            return View();
        }
	}
}