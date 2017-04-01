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
using Log.Common.Helper;
using Tracy.Frameworks.RabbitMQ;
using Log.Entity.RabbitMQ;

namespace Log.OpenApi.Controllers
{
    /// <summary>
    /// ErrorLog错误日志
    /// </summary>
    [RoutePrefix("api/errorlog")]
    public class ErrorLogController : BaseController
    {
        private static IConnection rabbitMQConn = RabbitMQHelper.CreateConnection();
        private static IRabbitMQWrapper _rabbitMQProxy;

        public ErrorLogController(IRabbitMQWrapper rabbitMQProxy)
        {
            _rabbitMQProxy = rabbitMQProxy;
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="debugLog"></param>
        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddLog(List<AddErrorLogRequest> list)
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
