using Log.Common.Helper;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tracy.Frameworks.RabbitMQ;
using Tracy.Frameworks.Common.Extends;
using Log.Entity.RabbitMQ;

namespace Log.OpenApi.Controllers
{
    /// <summary>
    /// xml和性能日志
    /// </summary>
    [RoutePrefix("api/xmlperformancelog")]
    public class XmlPerformanceLogController : BaseController
    {
        private static IConnection rabbitMQConn = RabbitMQHelper.CreateConnection();
        private static IRabbitMQWrapper _rabbitMQProxy;

        public XmlPerformanceLogController(IRabbitMQWrapper rabbitMQProxy)
        {
            _rabbitMQProxy = rabbitMQProxy;
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="debugLog"></param>
        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddLog(List<AddXmlPerformanceLogRequest> list)
        {
            //校验
            if (!list.HasValue())
            {
                return BadRequest();
            }

            using (var channel = rabbitMQConn.CreateModel())
            {
                foreach (var item in list)
                {
                    //分别写一条xml日志和一条性能日志
                    _rabbitMQProxy.Publish(item.XmlLog, channel);
                    _rabbitMQProxy.Publish(item.PerformanceLog, channel);
                }
            }

            return Ok();
        }

    }
}
