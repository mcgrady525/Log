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
            return new RabbitMQWrapper().CreateConnection(new RabbitMQConfig
            {
                Host = config.HostName,
                VirtualHost = config.VHost,
                HeartBeat = 60,
                AutomaticRecoveryEnabled = true,
                UserName = config.UserName,
                Password = config.Password
            });
        }

    }
}
