using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 获取黑名单列表(分页)response
    /// </summary>
    public class GetPagingDebugLogBlackListResponse
    {
        public long Id { get; set; }

        public string SystemCode { get; set; }

        public string Source { get; set; }

        public string MachineName { get; set; }

        public string IpAddress { get; set; }

        public string ClientIp { get; set; }

        public string AppdomainName { get; set; }

        public string Message { get; set; }

        public bool? IsRegex { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime? LastUpdatedTime { get; set; }

    }
}
