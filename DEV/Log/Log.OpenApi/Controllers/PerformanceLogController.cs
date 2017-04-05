using Log.Common.Helper;
using Log.Entity.RabbitMQ;
using Log.Entity.ViewModel;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Tracy.Frameworks.Common.Consts;
using Tracy.Frameworks.Common.Extends;
using Tracy.Frameworks.Configurations;
using Tracy.Frameworks.RabbitMQ;

namespace Log.OpenApi.Controllers
{
    /// <summary>
    /// PerformanceLog性能日志
    /// </summary>
    [RoutePrefix("api/performancelog")]
    public class PerformanceLogController : BaseController
    {
        private static IConnection rabbitMQConn = RabbitMQHelper.CreateConnection();
        private static IRabbitMQWrapper _rabbitMQProxy;

        public PerformanceLogController(IRabbitMQWrapper rabbitMQProxy)
        {
            _rabbitMQProxy = rabbitMQProxy;
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="debugLog"></param>
        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddLog(List<AddPerformanceLogRequest> list)
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
                    _rabbitMQProxy.Publish(item, channel);
                }
            }

            return Ok();//返回200成功状态码
        }
    }
}
