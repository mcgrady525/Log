﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tracy.Frameworks.RabbitMQ;

namespace Log.Entity.RabbitMQ
{
    /// <summary>
    /// 新增xml log request
    /// DTO
    /// </summary>
    [RabbitMQQueue("Log.Queue.XmlLog", ExchangeName = "Log.Exchange.XmlLog", IsProperties = true)]
    public class AddXmlLogRequest
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
        /// class_name
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// method_name
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// remark
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// created_time
        /// </summary>
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// rq
        /// </summary>
        public byte[] Rq { get; set; }

        /// <summary>
        /// rs
        /// </summary>
        public byte[] Rs { get; set; }

        /// <summary>
        /// client_ip
        /// </summary>
        public string ClientIp { get; set; }

        /// <summary>
        /// method_cname
        /// </summary>
        public string MethodCName { get; set; }

        /// <summary>
        /// 耗时，单位：ms
        /// 接口性能放在xml日志中
        /// </summary>
        public long Duration { get; set; }
    }
}
