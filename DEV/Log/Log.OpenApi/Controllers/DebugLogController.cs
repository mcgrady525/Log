﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tracy.Frameworks.LogClient.Entity;
using Tracy.Frameworks.LogClient.Helper;
using Tracy.Frameworks.Common.Extends;
using RabbitMQ.Client;
using System.Text;

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
        [HttpPost]
        public HttpResponseMessage AddLog(List<DebugLog> list)
        {
            //校验
            if (!list.HasValue())
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            //将数据放到rabbitMQ消息队列中
            var factory = new ConnectionFactory() { HostName = "127.0.0.1", Port = 5672, UserName = "admin", Password = "P@ssw0rd.123" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //声明交换机
                channel.ExchangeDeclare(exchange: "Log.Exchange.DebugLog", type: ExchangeType.Direct, durable: true);

                //声明队列
                channel.QueueDeclare(queue: "Log.Queue.DebugLog",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                //绑定
                channel.QueueBind(queue: "Log.Queue.DebugLog",
                                  exchange: "Log.Exchange.DebugLog",
                                  routingKey: "Log.Queue.DebugLog");

                //持久化
                var props = channel.CreateBasicProperties();
                props.Persistent = true;

                //发送消息
                //将批量转成单条发送
                foreach (var item in list)
                {
                    var msg = item.ToJson();
                    var body = Encoding.UTF8.GetBytes(msg);
                    channel.BasicPublish(exchange: "Log.Exchange.DebugLog",
                                         routingKey: "Log.Queue.DebugLog",
                                         basicProperties: props,
                                         body: body);
                }
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

    }
}
