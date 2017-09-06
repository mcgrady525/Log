using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tracy.Frameworks.Common.Helpers;

namespace Log.Site.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 清除全部缓存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ClearAllCache()
        {
            var flag = false;
            var msg = string.Empty;

            //清除全部缓存
            CacheHelper.RemoveAll();
            flag = true;

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

	}
}