using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracy.Frameworks.RabbitMQ;
using Tracy.Frameworks.Common.Extends;
using Tracy.Frameworks.Configurations;
using RabbitMQ.Client;


namespace Log.Common.Helper
{
    /// <summary>
    /// RabbitMQ helper
    /// </summary>
    public static partial class RabbitMQHelper
    {
        /// <summary>
        /// 创建rabbitMQ服务器链接
        /// </summary>
        /// <returns></returns>
        public static IConnection CreateConnection()
        {
            var config = System.Configuration.ConfigurationManager.GetSection("rabbitMQ") as RabbitMQConfigurationSection;
            var factory = new ConnectionFactory()
            {
                //设置主机名
                HostName = config.HostName,

                //设置VirtualHost
                VirtualHost = config.VHost.IsNullOrEmpty() ? "/" : config.VHost,

                //设置心跳时间
                RequestedHeartbeat = 60,

                //设置自动重连
                AutomaticRecoveryEnabled = true,

                //用户名
                UserName = config.UserName,

                //密码
                Password = config.Password
            };

            return factory.CreateConnection();
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <param name="command"></param>
        public static void Publish<T>(IModel channel, T command) where T : class
        {
            new RabbitMQWrapper().Publish(channel, command);
        }

    }
}
