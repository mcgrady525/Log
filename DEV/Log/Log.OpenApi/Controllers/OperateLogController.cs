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
    /// OperateLog操作日志
    /// </summary>
    [RoutePrefix("api/operatelog")]
    public class OperateLogController : BaseController
    {
        private static IConnection rabbitMQConn = RabbitMQHelper.CreateConnection();
        private static IRabbitMQWrapper _rabbitMQProxy;

        public OperateLogController(IRabbitMQWrapper rabbitMQProxy)
        {
            _rabbitMQProxy = rabbitMQProxy;
        }

        /// <summary>
        /// 新增日志到消息队列
        /// </summary>
        /// <param name="debugLog"></param>
        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddLog(List<AddOperateLogRequest> list)
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
                    _rabbitMQProxy.Publish(item, channel);
                }
            }

            return Ok();
        }
    }
}
