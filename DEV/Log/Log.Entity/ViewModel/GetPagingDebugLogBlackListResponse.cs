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

        public string Content { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime? LastUpdatedTime { get; set; }

    }
}
