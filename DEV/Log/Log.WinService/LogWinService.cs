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
using Tracy.Frameworks.Common.Extends;
using Log.IService;
using Tracy.Frameworks.Common.Consts;
using Log.Common.Helper;
using Tracy.Frameworks.Configurations;
using System.Configuration;
using Log.Entity.ViewModel;
using Autofac;
using System.Reflection;
using System.Threading;

namespace Log.WinService
{
    public partial class LogWinService : ServiceBase
    {
        private IConnection connection = null;
        private Autofac.IContainer container = null;
        private List<Task> tasks = null;
        private CancellationTokenSource cancelToken = null;

        public LogWinService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                tasks = new List<Task>();
                cancelToken = new CancellationTokenSource();

                //Autofac初始化
                var builder = new ContainerBuilder();
                var iDao = Assembly.Load("Log.IDao");
                var dao = Assembly.Load("Log.Dao");
                var iService = Assembly.Load("Log.IService");
                var service = Assembly.Load("Log.Service");
                builder.RegisterAssemblyTypes(iDao, dao).Where(t => t.Name.EndsWith("Dao")).AsImplementedInterfaces();
                builder.RegisterAssemblyTypes(iService, service).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();
                container = builder.Build();

                //用多线程去分别消费各队列的消息
                //共用connection，各线程单独创建channel
                //windows服务默认的是后台线程
                LogHelper.Info(() => "开始启动LogWinService服务!");
                var rabbitMQConfig = ConfigurationManager.GetSection("rabbitMQ") as RabbitMQConfigurationSection;
                var factory = new ConnectionFactory() { HostName = rabbitMQConfig.HostName, Port = rabbitMQConfig.Port, UserName = rabbitMQConfig.UserName, Password = rabbitMQConfig.Password, VirtualHost = rabbitMQConfig.VHost };
                connection = factory.CreateConnection();

                //消费debug log
                var debugLogTask = System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    ConsumerDebugLogMessage(connection, cancelToken);
                }, TaskCreationOptions.LongRunning);
                tasks.Add(debugLogTask);

                //消费error log
                var errorLogTask = System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    ConsumerErrorLogMessage(connection, cancelToken);
                }, TaskCreationOptions.LongRunning);
                tasks.Add(errorLogTask);

                //消费xml log
                var xmlLogTask = System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    ConsumerXmlLogMessage(connection, cancelToken);
                }, TaskCreationOptions.LongRunning);
                tasks.Add(xmlLogTask);

                //消费perf log
                var perfLogTask = System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    ConsumerPerfLogMessage(connection, cancelToken);
                }, TaskCreationOptions.LongRunning);
                tasks.Add(perfLogTask);

                LogHelper.Info(() => "LogWinService服务启动成功!");
            }
            catch (Exception ex)
            {
                LogHelper.Error(() => string.Format("启动LogWinService服务失败，失败原因：{0}", ex.ToString()));
            }
        }

        protected override void OnStop()
        {
            //关闭rabbitMQ服务器连接
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
            }

            //重置Autofac容器
            container = null;

            //协作停止异步线程任务
            cancelToken.Cancel();

            LogHelper.Info(() => "LogWinService服务已停止!");
        }

        /// <summary>
        /// 消费性能日志消息
        /// </summary>
        /// <param name="connection"></param>
        private void ConsumerPerfLogMessage(IConnection connection, CancellationTokenSource cancelToken)
        {
            try
            {
                LogHelper.Info(() => "开始消费性能日志消息!");
                ILogsPerformanceLogService _perfLogService;
                using (var scope = container.BeginLifetimeScope())
                {
                    _perfLogService = scope.Resolve<ILogsPerformanceLogService>();
                }

                using (var channel = connection.CreateModel())
                {
                    //声明队列
                    channel.QueueDeclare(queue: RabbitMQQueueConst.LogPerformanceLog,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                    //设置公平调度
                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    //消费消息
                    //EventingBasicConsumer的Received事件无法触发
                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(queue: RabbitMQQueueConst.LogPerformanceLog, noAck: false, consumer: consumer);
                    while (true)
                    {
                        if (cancelToken.IsCancellationRequested)
                        {
                            break;
                        }

                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        var msg = Encoding.UTF8.GetString(ea.Body);

                        //反序列化并持久化到数据中
                        var perfLog = msg.FromJson<AddPerformanceLogRequest>();
                        _perfLogService.AddPerfLog(perfLog);

                        channel.BasicAck(ea.DeliveryTag, multiple: false);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(()=>string.Format("消费性能日志消息时发生异常，详细信息：", ex.ToString()));
            }
        }

        /// <summary>
        /// 消费xml日志消息
        /// </summary>
        /// <param name="connection"></param>
        private void ConsumerXmlLogMessage(IConnection connection, CancellationTokenSource cancelToken)
        {
            try
            {
                LogHelper.Info(() => "开始消费Xml日志消息!");
                ILogsXmlLogService _xmlLogService;
                using (var scope = container.BeginLifetimeScope())
                {
                    _xmlLogService = scope.Resolve<ILogsXmlLogService>();
                }

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
                        if (cancelToken.IsCancellationRequested)
                        {
                            break;
                        }

                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        var msg = Encoding.UTF8.GetString(ea.Body);

                        //反序列化并持久化到数据中
                        var xmlLog = msg.FromJson<AddXmlLogRequest>();
                        _xmlLogService.AddXmlLog(xmlLog);

                        channel.BasicAck(ea.DeliveryTag, multiple: false);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(()=>string.Format("消费Xml日志消息时发生异常，详细信息：", ex.ToString()));
            }
        }

        /// <summary>
        /// 消费错误日志消息
        /// </summary>
        /// <param name="connection"></param>
        private void ConsumerErrorLogMessage(IConnection connection, CancellationTokenSource cancelToken)
        {
            try
            {
                LogHelper.Info(() => "开始消费错误日志消息!");
                ILogsErrorLogService _errorLogService;
                using (var scope = container.BeginLifetimeScope())
                {
                    _errorLogService = scope.Resolve<ILogsErrorLogService>();
                }

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
                        if (cancelToken.IsCancellationRequested)
                        {
                            break;
                        }

                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        var msg = Encoding.UTF8.GetString(ea.Body);

                        //反序列化并持久化到数据中
                        var errorLog = msg.FromJson<AddErrorLogRequest>();
                        _errorLogService.AddErrorLog(errorLog);

                        channel.BasicAck(ea.DeliveryTag, multiple: false);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(()=>string.Format("消费错误日志消息时发生异常，详细信息：", ex.ToString()));
            }
        }

        /// <summary>
        /// 消费调试日志消息
        /// </summary>
        /// <param name="connection"></param>
        private void ConsumerDebugLogMessage(IConnection connection, CancellationTokenSource cancelToken)
        {
            try
            {
                LogHelper.Info(() => "开始消费调试日志消息!");
                ILogsDebugLogService _debugLogService;
                using (var scope = container.BeginLifetimeScope())
                {
                    _debugLogService = scope.Resolve<ILogsDebugLogService>();
                }

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
                        if (cancelToken.IsCancellationRequested)
                        {
                            break;
                        }

                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        var msg = Encoding.UTF8.GetString(ea.Body);

                        //反序列化并持久化到数据中
                        var debugLog = msg.FromJson<AddDebugLogRequest>();
                        _debugLogService.AddDebugLog(debugLog);

                        channel.BasicAck(ea.DeliveryTag, multiple: false);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(() => string.Format("消费调试日志消息时发生异常，详细信息：{0}", ex.ToString()));
            }
        }
    }
}
