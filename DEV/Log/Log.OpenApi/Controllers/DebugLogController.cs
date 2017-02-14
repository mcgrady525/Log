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
using Tracy.Frameworks.Common.Consts;

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
        public IHttpActionResult AddLog(List<DebugLog> list)
        {
            //校验
            if (!list.HasValue())
            {
                return BadRequest();//返回400错误
            }

            //将数据放到rabbitMQ消息队列中
            var factory = new ConnectionFactory() { HostName = "127.0.0.1", Port = 5672, UserName = "admin", Password = "P@ssw0rd.123" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //声明交换机
                channel.ExchangeDeclare(exchange: RabbitMQExchangeConst.LogDebugLog, type: ExchangeType.Direct, durable: true);

                //声明队列
                channel.QueueDeclare(queue: RabbitMQQueueConst.LogDebugLog,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                //绑定
                channel.QueueBind(queue: RabbitMQQueueConst.LogDebugLog,
                                  exchange: RabbitMQExchangeConst.LogDebugLog,
                                  routingKey: RabbitMQQueueConst.LogDebugLog);

                //持久化
                var props = channel.CreateBasicProperties();
                props.Persistent = true;

                //发送消息
                //将批量转成单条发送
                foreach (var item in list)
                {
                    var msg = item.ToJson();
                    var body = Encoding.UTF8.GetBytes(msg);
                    channel.BasicPublish(exchange: RabbitMQExchangeConst.LogDebugLog,
                                         routingKey: RabbitMQQueueConst.LogDebugLog,
                                         basicProperties: props,
                                         body: body);
                }
            }

            return Ok();//返回200成功状态码
        }

    }
}
