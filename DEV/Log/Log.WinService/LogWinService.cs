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

namespace Log.WinService
{
    public partial class LogWinService : ServiceBase
    {
        public LogWinService()
        {
            InitializeComponent();
        }
        IConnection connection = null;

        protected override void OnStart(string[] args)
        {
            try
            {
                //用多线程去分别消费各队列的消息
                //共用connection，各线程单独创建channel
                //windows服务默认的是后台线程
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
            catch (Exception ex)
            {
                WriteLogs(string.Format("启动LogWinService服务失败，失败原因：{0}", ex.ToString()));
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

            WriteLogs("LogWinService服务已停止!");
        }

        /// <summary>
        /// 消费错误日志消息
        /// </summary>
        /// <param name="connection"></param>
        private void ConsumerErrorLogMessage(IConnection connection)
        {
            WriteLogs("开始消费错误日志消息!");
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// 消费调试日志消息
        /// </summary>
        /// <param name="connection"></param>
        private void ConsumerDebugLogMessage(IConnection connection)
        {
            WriteLogs("开始消费调试日志消息!");
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
                //EventingBasicConsumer的Received事件无法触发
                var consumer = new QueueingBasicConsumer(channel);
                channel.BasicConsume(queue: "Log.Queue.DebugLog", noAck: false, consumer: consumer);
                while (true)
                {
                    var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                    var msg = Encoding.UTF8.GetString(ea.Body);

                    //写到文本文件，后面会写到数据库中
                    WriteLogs(msg);

                    channel.BasicAck(ea.DeliveryTag, multiple: false);
                }
            }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="msg"></param>
        private void WriteLogs(string msg)
        {
            msg = string.Format("【{0}】{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), msg);
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
