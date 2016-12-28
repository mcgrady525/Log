using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 删除角色request
    /// </summary>
    [Serializable]
    public class DeleteRoleRequest
    {
        /// <summary>
        /// 待删除的角色id
        /// </summary>
        public int DeleteRoleId { get; set; }

    }
}
