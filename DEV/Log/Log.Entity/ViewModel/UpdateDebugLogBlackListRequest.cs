using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 修改debug log黑名单request
    /// </summary>
    public class UpdateDebugLogBlackListRequest
    {
        public long Id { get; set; }

        public string Content { get; set; }

    }
}
