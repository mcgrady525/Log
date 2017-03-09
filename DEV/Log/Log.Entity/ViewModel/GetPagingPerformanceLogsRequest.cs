using Log.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 获取所有性能日志(分页)request
    /// </summary>
    public class GetPagingPerformanceLogsRequest : PagingBase
    {
        public string SystemCode { get; set; }

        public string Source { get; set; }

        public string ClassName { get; set; }

        public string MethodName { get; set; }

        public DateTime? CreatedTimeStart { get; set; }

        public DateTime? CreatedTimeEnd { get; set; }
    }
}
