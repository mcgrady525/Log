using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Log.Entity.Common;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 获取指定角色下的所有用户request
    /// </summary>
    [Serializable]
    public class GetPagingRoleUsersRequest : PagingBase
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public int RoleId { get; set; }
    }
}
