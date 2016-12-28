using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 角色授权request
    /// </summary>
    [Serializable]
    public class AuthorizeRoleRequest
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 该角色选择的菜单和按钮,格式：5,1|5,2|7,1|10,1|11,1分别是菜单id、按钮id
        /// </summary>
        public string MenuButtonId { get; set; }

    }
}
