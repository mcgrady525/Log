using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 设置角色request
    /// </summary>
    [Serializable]
    public class SetRoleRequest
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
        /// 角色id，多个角色以','分隔
        /// </summary>
        public string RoleIds { get; set; }

    }
}
