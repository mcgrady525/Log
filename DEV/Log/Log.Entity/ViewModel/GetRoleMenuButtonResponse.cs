using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 角色授权页面，获取角色所拥有的菜单按钮权限request
    /// </summary>
    public class GetRoleMenuButtonResponse
    {
        /// <summary>
        /// 菜单id
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 上级菜单id
        /// </summary>
        public int MenuParentId { get; set; }

        /// <summary>
        /// 菜单icon
        /// </summary>
        public string MenuIcon { get; set; }

        /// <summary>
        /// 按钮id
        /// </summary>
        public int? ButtonId { get; set; }

        /// <summary>
        /// 按钮名称
        /// </summary>
        public string ButtonName { get; set; }

        /// <summary>
        /// 按钮icon
        /// </summary>
        public string ButtonIcon { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public int? RoleId { get; set; }

        /// <summary>
        /// 是否勾选(按钮)
        /// 必须使用字符串格式的'true'或'false'，否则绑定tree时有问题
        /// </summary>
        public string Checked { get; set; }
    }
}
