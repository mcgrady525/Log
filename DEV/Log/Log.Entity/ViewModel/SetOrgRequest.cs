using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 设置机构request
    /// </summary>
    public class SetOrgRequest
    {
        /// <summary>
        /// 用户id，多个用户以','分隔
        /// </summary>
        public string UserIds { get; set; }

        /// <summary>
        /// 用户名，多个用户以','分隔
        /// </summary>
        public string UserNames { get; set; }

        /// <summary>
        /// 机构id，多个机构以','分隔
        /// </summary>
        public string OrgIds { get; set; }
    }
}
