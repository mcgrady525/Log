using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 获取所有调试日志(分页)response
    /// </summary>
    public class GetPagingDebugLogsResponse
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
        /// ip
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 应用程序域
        /// </summary>
        public string AppDomainName { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public byte[] Message { get; set; }

        public string MessageDetail { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// 日志详情url，格式：http://localhost.dev.ssharing.com:8080/LogSite/DebugLog/detail/1
        /// </summary>
        public string DetailUrl { get; set; }

    }
}
