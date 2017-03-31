using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tracy.Frameworks.Common.Extends;
using RabbitMQ.Client;
using System.Text;
using Tracy.Frameworks.Common.Consts;
using System.Configuration;
using Tracy.Frameworks.Configurations;
using Log.Entity.ViewModel;
using Tracy.Frameworks.RabbitMQ;
using Log.Common.Helper;

namespace Log.OpenApi.Controllers
{
    /// <summary>
    /// DebugLog调试日志
    /// </summary>
    [RoutePrefix("api/debuglog")]
    public class DebugLogController : BaseController
    {
        //rabbitMQ连接
        private static readonly IConnection rabbitMQConn = RabbitMQHelper.CreateConnection();
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="debugLog"></param>
        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddLog(List<AddDebugLogRequest> list)
        {
            //校验
            if (!list.HasValue())
            {
                return BadRequest();//返回400错误
            }

            using (var channel = rabbitMQConn.CreateModel())
            {
                foreach (var item in list)
                {
                    RabbitMQHelper.Publish(channel, item);
                }
            }

            return Ok();//返回200成功状态码
        }

    }
}
