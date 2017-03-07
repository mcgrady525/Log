using Log.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 获取所有错误日志(分页)request
    /// </summary>
    public class GetPagingErrorLogsRequest : PagingBase
    {
        public string SystemCode { get; set; }

        public string Source { get; set; }

        public string Message { get; set; }

        public DateTime? CreatedTimeStart { get; set; }

        public DateTime? CreatedTimeEnd { get; set; }
    }
}
