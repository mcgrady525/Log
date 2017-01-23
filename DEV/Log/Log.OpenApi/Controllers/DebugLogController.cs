using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tracy.Frameworks.LogClient.Entity;
using Tracy.Frameworks.LogClient.Helper;
using Tracy.Frameworks.Common.Extends;

namespace Log.OpenApi.Controllers
{
    /// <summary>
    /// DebugLog调试日志
    /// </summary>
    [RoutePrefix("api/debuglog")]
    public class DebugLogController : BaseController
    {
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="debugLog"></param>
        [Route("add")]
        public HttpResponseMessage AddLog(DebugLog debugLog)
        {
            //将数据放到rabbitMQ消息队列中



            return new HttpResponseMessage(HttpStatusCode.OK);
        }

    }
}
