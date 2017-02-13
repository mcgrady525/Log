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

        }
    }
}
