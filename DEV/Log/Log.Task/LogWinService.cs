using Log.Common;
using Quartz;
using Quartz.Impl;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using RabbitMQ.Client.Events;

namespace Log.Task
{
    public partial class LogWinService : ServiceBase
    {
        public LogWinService()
        {
            InitializeComponent();
        }
        IConnection connection = null;

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            //用多线程去分别消费各队列的消息
            //共用connection，各线程单独创建channel
            WriteLogs("开始启动LogWinService服务!");
            var factory = new ConnectionFactory() { HostName = "127.0.0.1", Port = 5672, UserName = "admin", Password = "P@ssw0rd.123" };
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

            WriteLogs("LogWinService服务启动成功!");
        }

        /// <summary>
        /// 停止
        /// </summary>
        protected override void OnStop()
        {
            //关闭连接
            if (connection != null)
            {
                connection.Dispose();
            }

            WriteLogs("LogWinService服务已停止!");
        }

        /// <summary>
        /// 消费错误日志消息
        /// </summary>
        /// <param name="connection"></param>
        private void ConsumerErrorLogMessage(IConnection connection)
        {

        }

        /// <summary>
        /// 消费调试日志消息
        /// </summary>
        /// <param name="connection"></param>
        private void ConsumerDebugLogMessage(IConnection connection)
        {
            using (var channel = connection.CreateModel())
            {
                //声明队列
                channel.QueueDeclare(queue: "Log.Queue.DebugLog",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                //设置公平调度
                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                //消费消息
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var msg = Encoding.UTF8.GetString(ea.Body);

                    //先写到日志里，后面要保存到数据库中
                    //Console.WriteLine("[x] Received {0}", msg);
                    WriteLogs(msg);

                    channel.BasicAck(ea.DeliveryTag, multiple: false);
                };
                channel.BasicConsume(queue: "Log.Queue.DebugLog", noAck: false, consumer: consumer);
            }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="msg"></param>
        private void WriteLogs(string msg)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "LogWinService.log";
            var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            var sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(msg);

            sw.Flush();
            sw.Close();
            fs.Close();
        }
    }
}
