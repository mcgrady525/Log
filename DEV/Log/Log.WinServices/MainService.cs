using Autofac;
using Log.Common.Helper;
using Log.Entity.ViewModel;
using Log.IService;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tracy.Frameworks.Configurations;
using Tracy.Frameworks.Common.Extends;
using Tracy.Frameworks.RabbitMQ;
using Log.Entity.RabbitMQ;

namespace Log.WinServices
{
    public class MainService
    {
        //rabbitMQ连接
        private RabbitMQWrapper rabbitMQProxy;
        private Autofac.IContainer container = null;
        private List<Task> tasks = null;
        private CancellationTokenSource cancelToken = null;

        public MainService()
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

            //rabbitMQ初始化
            var config = System.Configuration.ConfigurationManager.GetSection("rabbitMQ") as RabbitMQConfigurationSection;
            rabbitMQProxy = new RabbitMQWrapper(new RabbitMQConfig
            {
                Host = config.HostName,
                VirtualHost = config.VHost,
                HeartBeat = 60,
                AutomaticRecoveryEnabled = true,
                UserName = config.UserName,
                Password = config.Password
            });
        }

        public bool Start()
        {
            //用多线程去分别消费各队列的消息
            LogHelper.Info(() => "开始启动LogWinServices服务!");

            //消费debug log
            var debugLogTask = System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                ConsumerDebugLogMessage();
            }, TaskCreationOptions.LongRunning);
            tasks.Add(debugLogTask);

            //消费error log
            var errorLogTask = System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                ConsumerErrorLogMessage();
            }, TaskCreationOptions.LongRunning);
            tasks.Add(errorLogTask);

            //消费xml log
            var xmlLogTask = System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                ConsumerXmlLogMessage();
            }, TaskCreationOptions.LongRunning);
            tasks.Add(xmlLogTask);

            //消费perf log
            var perfLogTask = System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                ConsumerPerfLogMessage();
            }, TaskCreationOptions.LongRunning);
            tasks.Add(perfLogTask);

            LogHelper.Info(() => "LogWinServices服务启动成功!");

            return true;
        }

        public bool Stop()
        {
            //重置Autofac容器
            container = null;
            LogHelper.Info(() => "LogWinServices服务已停止!");

            //rabbitMQ资源释放
            rabbitMQProxy.Dispose();

            return true;
        }

        /// <summary>
        /// 消费性能日志消息
        /// </summary>
        /// <param name="connection"></param>
        private void ConsumerPerfLogMessage()
        {
            try
            {
                LogHelper.Info(() => "开始消费性能日志消息!");
                ILogsPerformanceLogService _perfLogService;
                using (var scope = container.BeginLifetimeScope())
                {
                    _perfLogService = scope.Resolve<ILogsPerformanceLogService>();
                }

                //使用发布/订阅模式消费消息
                rabbitMQProxy.Subscribe<AddPerformanceLogRequest>(item =>
                {
                    _perfLogService.AddPerfLog(item);
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error(() => string.Format("消费性能日志消息时发生异常，详细信息：", ex.ToString()));
            }
        }

        /// <summary>
        /// 消费xml日志消息
        /// </summary>
        /// <param name="connection"></param>
        private void ConsumerXmlLogMessage()
        {
            try
            {
                LogHelper.Info(() => "开始消费Xml日志消息!");
                ILogsXmlLogService _xmlLogService;
                using (var scope = container.BeginLifetimeScope())
                {
                    _xmlLogService = scope.Resolve<ILogsXmlLogService>();
                }

                //使用发布/订阅模式消费消息
                rabbitMQProxy.Subscribe<AddXmlLogRequest>(item =>
                {
                    _xmlLogService.AddXmlLog(item);
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error(() => string.Format("消费Xml日志消息时发生异常，详细信息：", ex.ToString()));
            }
        }

        /// <summary>
        /// 消费错误日志消息
        /// </summary>
        /// <param name="connection"></param>
        private void ConsumerErrorLogMessage()
        {
            try
            {
                LogHelper.Info(() => "开始消费错误日志消息!");
                ILogsErrorLogService _errorLogService;
                using (var scope = container.BeginLifetimeScope())
                {
                    _errorLogService = scope.Resolve<ILogsErrorLogService>();
                }

                //使用发布/订阅模式消费消息
                rabbitMQProxy.Subscribe<AddErrorLogRequest>(item =>
                {
                    _errorLogService.AddErrorLog(item);
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error(() => string.Format("消费错误日志消息时发生异常，详细信息：", ex.ToString()));
            }
        }

        /// <summary>
        /// 消费调试日志消息
        /// </summary>
        /// <param name="connection"></param>
        private void ConsumerDebugLogMessage()
        {
            try
            {
                LogHelper.Info(() => "开始消费调试日志消息!");
                ILogsDebugLogService _debugLogService;
                using (var scope = container.BeginLifetimeScope())
                {
                    _debugLogService = scope.Resolve<ILogsDebugLogService>();
                }

                //使用发布/订阅模式消费消息
                rabbitMQProxy.Subscribe<AddDebugLogRequest>(item =>
                {
                    _debugLogService.AddDebugLog(item);
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error(() => string.Format("消费调试日志消息时发生异常，详细信息：{0}", ex.ToString()));
            }
        }

    }
}
