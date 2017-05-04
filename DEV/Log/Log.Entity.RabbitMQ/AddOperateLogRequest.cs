using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tracy.Frameworks.RabbitMQ;

namespace Log.Entity.RabbitMQ
{
    /// <summary>
    /// 新增operate log request
    /// DTO
    /// </summary>
    [RabbitMQQueue("Log.Queue.OperateLog", ExchangeName = "Log.Exchange.OperateLog", IsProperties = true)]
    public class AddOperateLogRequest
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
        /// OperatedTime
        /// </summary>
        public DateTime? OperatedTime { get; set; }

        /// <summary>
        /// user_id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// user_name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// operate_module
        /// </summary>
        public string OperateModule { get; set; }

        /// <summary>
        /// operate_type
        /// </summary>
        public string OperateType { get; set; }

        /// <summary>
        /// modify_before
        /// </summary>
        public byte[] ModifyBefore { get; set; }

        /// <summary>
        /// modify_after
        /// </summary>
        public byte[] ModifyAfter { get; set; }

        /// <summary>
        /// created_time
        /// </summary>
        public DateTime? CreatedTime { get; set; }

    }
}
