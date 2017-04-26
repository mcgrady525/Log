using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 添加error log黑名单request
    /// </summary>
    public class InsertErrorLogBlackListRequest
    {
        public string SystemCode { get; set; }

        public string Source { get; set; }

        public string MachineName { get; set; }

        public string IpAddress { get; set; }

        public string ClientIp { get; set; }

        public string AppdomainName { get; set; }

        public string Message { get; set; }

        public bool? IsRegex { get; set; }

    }
}
