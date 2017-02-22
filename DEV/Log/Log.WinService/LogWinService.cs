using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Log.Service;
using Tracy.Frameworks.Common.Extends;
using Log.IService;
using Tracy.Frameworks.Common.Consts;
using Log.Common.Helper;
using Tracy.Frameworks.Configurations;
using System.Configuration;
using Log.Entity.ViewModel;

namespace Log.WinService
{
    public partial class LogWinService : ServiceBase
    {
        //注入service
        private static readonly ILogsDebugLogService debugLogService = new LogsDebugLogService();
        private static readonly ILogsErrorLogService errorLogService = new LogsErrorLogService();
        private static readonly ILogsXmlLogService xmlLogService = new LogsXmlLogService();

        IConnection connection = null;
        public LogWinService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                //用多线程去分别消费各队列的消息
                //共用connection，各线程单独创建channel
                //windows服务默认的是后台线程
                LogHelper.Info(() => "开始启动LogWinService服务!");
                var rabbitMQConfig = ConfigurationManager.GetSection("rabbitMQ") as RabbitMQConfigurationSection;
                var factory = new ConnectionFactory() { HostName = rabbitMQConfig.HostName, Port = rabbitMQConfig.Port, UserName = rabbitMQConfig.UserName, Password = rabbitMQConfig.Password };
                connection = factory.CreateConnection();

                //消费debug log
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    ConsumerDebugLogMessage(connection);
                });

                //消费error log
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    ConsumerErrorLogMessage(connection);
                });

                //消费xml log
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    ConsumerXmlLogMessage(connection);
                });

                LogHelper.Info(() => "LogWinService服务启动成功!");
            }
            catch (Exception ex)
            {
                LogHelper.Error(() => string.Format("启动LogWinService服务失败，失败原因：{0}", ex.ToString()));
            }
        }

        protected override void OnStop()
        {
            //关闭连接
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
            }

            LogHelper.Info(() => "LogWinService服务已停止!");
        }

        /// <summary>
        /// 消费xml日志消息
        /// </summary>
        /// <param name="connection"></param>
        private void ConsumerXmlLogMessage(IConnection connection)
        {
            LogHelper.Info(() => "开始消费Xml日志消息!");
            using (var channel = connection.CreateModel())
            {
                //声明队列
                channel.QueueDeclare(queue: RabbitMQQueueConst.LogXmlLog,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                //设置公平调度
                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                //消费消息
                //EventingBasicConsumer的Received事件无法触发
                var consumer = new QueueingBasicConsumer(channel);
                channel.BasicConsume(queue: RabbitMQQueueConst.LogXmlLog, noAck: false, consumer: consumer);
                while (true)
                {
                    var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                    var msg = Encoding.UTF8.GetString(ea.Body);

                    //反序列化并持久化到数据中
                    var xmlLog = msg.FromJson<AddXmlLogRequest>();
                    xmlLogService.AddXmlLog(xmlLog);

                    channel.BasicAck(ea.DeliveryTag, multiple: false);
                }
            }
        }

        /// <summary>
        /// 消费错误日志消息
        /// </summary>
        /// <param name="connection"></param>
        private void ConsumerErrorLogMessage(IConnection connection)
        {
            LogHelper.Info(() => "开始消费错误日志消息!");
            using (var channel = connection.CreateModel())
            {
                //声明队列
                channel.QueueDeclare(queue: RabbitMQQueueConst.LogErrorLog,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                //设置公平调度
                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                //消费消息
                //EventingBasicConsumer的Received事件无法触发
                var consumer = new QueueingBasicConsumer(channel);
                channel.BasicConsume(queue: RabbitMQQueueConst.LogErrorLog, noAck: false, consumer: consumer);
                while (true)
                {
                    var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                    var msg = Encoding.UTF8.GetString(ea.Body);

                    //反序列化并持久化到数据中
                    var errorLog = msg.FromJson<AddErrorLogRequest>();
                    errorLogService.AddErrorLog(errorLog);

                    channel.BasicAck(ea.DeliveryTag, multiple: false);
                }
            }
        }

        /// <summary>
        /// 消费调试日志消息
        /// </summary>
        /// <param name="connection"></param>
        private void ConsumerDebugLogMessage(IConnection connection)
        {
            LogHelper.Info(() => "开始消费调试日志消息!");
            using (var channel = connection.CreateModel())
            {
                //声明队列
                channel.QueueDeclare(queue: RabbitMQQueueConst.LogDebugLog,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                //设置公平调度，每次处理一条
                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                //消费消息
                //EventingBasicConsumer的Received事件无法触发
                var consumer = new QueueingBasicConsumer(channel);
                channel.BasicConsume(queue: RabbitMQQueueConst.LogDebugLog, noAck: false, consumer: consumer);
                while (true)
                {
                    var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                    var msg = Encoding.UTF8.GetString(ea.Body);

                    //反序列化并持久化到数据中
                    var debugLog = msg.FromJson<AddDebugLogRequest>();
                    debugLogService.AddDebugLog(debugLog);

                    channel.BasicAck(ea.DeliveryTag, multiple: false);
                }
            }
        }
    }
}
