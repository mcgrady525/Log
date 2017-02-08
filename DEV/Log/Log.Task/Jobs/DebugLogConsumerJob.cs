using Quartz;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace Log.Task
{
    /// <summary>
    /// 调试日志rabbitMQ消费消息
    /// </summary>
    public class DebugLogConsumerJob : IStatefulJob
    {
        public void Execute(JobExecutionContext context)
        {
            var factory = new ConnectionFactory() { HostName = "127.0.0.1", Port = 5672, UserName = "admin", Password = "P@ssw0rd.123" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //声明队列
                channel.QueueDeclare(queue: "Log.Queue.DebugLog",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                //公平调度
                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var msg = Encoding.UTF8.GetString(ea.Body);

                    //先写到日志里，后面要保存到数据库中

                    Console.WriteLine("[x] Received {0}", msg);
                    channel.BasicAck(ea.DeliveryTag, multiple: false);
                };

                channel.BasicConsume(queue: "Log.Queue.DebugLog", noAck: false,consumer: consumer);
            }
        }
    }
}
