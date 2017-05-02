using Log.Entity.RabbitMQ;
using Log.IService;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracy.Frameworks.Common.Extends;
using Log.Common.Helper;
using System.Threading;
using Autofac;
using System.Reflection;

namespace Log.WinServices.Manager
{
    /// <summary>
    /// DebugLog日志manager
    /// </summary>
    public class DebugLogManager
    {
        private static ConcurrentQueue<AddDebugLogRequest> DebugLogQueue = new ConcurrentQueue<AddDebugLogRequest>();

        /// <summary>
        /// 1，要能实时监控该task的状态。
        /// 2，当task挂掉的时候能及时通知。
        /// </summary>
        private static readonly Task DebugLogTask = Task.Factory.StartNew(() =>
        {
            WriteLog();
        }, TaskCreationOptions.LongRunning);

        private static IContainer container = null;
        private static ILogsDebugLogService _debugLogService;

        static DebugLogManager()
        {
            //Autofac初始化
            container = LogNewHelper.BuildAutofacContainer();

            using (var scope = container.BeginLifetimeScope())
            {
                _debugLogService = scope.Resolve<ILogsDebugLogService>();
            }
        }

        /// <summary>
        /// 写日志(异步)
        /// </summary>
        public static void WriteLog()
        {
            //先出队，然后批量插入数据库
            var insertCycleTime = ConfigHelper.LogInsertCycleTime;
            while (true)
            {
                try
                {
                    var list = Dequeue();
                    if (list.HasValue())
                    {
                        _debugLogService.AddDebugLogs(list);
                    }
                }
                catch (Exception ex)
                {
                    //写日志
                    LogHelper.Error(() => string.Format("异步从本地队列写日志到数据库发生异常，详情：{0}", ex.ToString()));
                }

                Thread.Sleep(insertCycleTime);
            }
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <returns></returns>
        public static List<AddDebugLogRequest> Dequeue()
        {
            //从队列中取出一批消息
            var result = new List<AddDebugLogRequest>();
            AddDebugLogRequest item = null;

            while (true)
            {
                if (DebugLogQueue.TryDequeue(out item))
                {
                    result.Add(item);
                    if (result.Count >= ConfigHelper.LogMaxPostCount)
                    {
                        break;
                    }
                }
                else
                {
                    //队列中没有消息直接返回
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="list"></param>
        public static void Enqueue(AddDebugLogRequest item)
        {
            //只有Task在运行时才往队列中添加消息
            if (DebugLogTask.Status == TaskStatus.Running)
            {
                DebugLogQueue.Enqueue(item);
            }
        }
    }
}
