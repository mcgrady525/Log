using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 删除debug log黑名单request
    /// </summary>
    public class DeleteDebugLogBlackListRequest
    {
        /// <summary>
        /// 待删除的id，多个id以','分隔
        /// </summary>
        public string Ids { get; set; }
    }
}
