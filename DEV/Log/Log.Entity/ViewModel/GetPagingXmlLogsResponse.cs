using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 获取所有xml日志(分页)response
    /// </summary>
    public class GetPagingXmlLogsResponse
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 系统代码
        /// </summary>
        public string SystemCode { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// ClassName
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// MethodName
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// ip
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 应用程序域
        /// </summary>
        public string AppDomainName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// 日志详情url，格式：http://localhost.dev.ssharing.com:8080/LogSite/ErrorLog/detail/1
        /// </summary>
        public string DetailUrl { get; set; }

        /// <summary>
        /// methodName中文
        /// </summary>
        public string MethodCName { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        public string ClientIp { get; set; }

        /// <summary>
        /// MachineName
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        /// 耗时，单位：ms
        /// </summary>
        public long Duration { get; set; }

        public string DurationToStringN
        {
            get
            {
                return Duration.ToString("N0");
            }
        }

    }
}
