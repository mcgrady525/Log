using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tracy.Frameworks.RabbitMQ;

namespace Log.Entity.RabbitMQ
{
    /// <summary>
    /// 新增error log request
    /// DTO
    /// </summary>
    [RabbitMQQueue("Log.Queue.ErrorLog", ExchangeName = "Log.Exchange.ErrorLog", IsProperties = true)]
    public class AddErrorLogRequest
    {
        /// <summary>
        /// system_code
        /// </summary>
        public string SystemCode { get; set; }

        /// <summary>
        /// source
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// machine_name
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        /// ip_address
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// process_id
        /// </summary>
        public int? ProcessId { get; set; }

        /// <summary>
        /// process_name
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// thread_id
        /// </summary>
        public int? ThreadId { get; set; }

        /// <summary>
        /// thread_name
        /// </summary>
        public string ThreadName { get; set; }

        /// <summary>
        /// appdomain_name
        /// </summary>
        public string AppdomainName { get; set; }

        /// <summary>
        /// message
        /// </summary>
        public byte[] Message { get; set; }

        /// <summary>
        /// created_time
        /// </summary>
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// detail
        /// </summary>
        public byte[] Detail { get; set; }
    }
}
