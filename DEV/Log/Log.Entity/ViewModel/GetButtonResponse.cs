using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 获取当前菜单关联的按钮response
    /// </summary>
    [Serializable]
    public class GetButtonResponse
    {
        /// <summary>
        /// 菜单id
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// 按钮id
        /// </summary>
        public int ButtonId { get; set; }

        /// <summary>
        /// 按钮名称
        /// </summary>
        public string ButtonName { get; set; }

        /// <summary>
        /// 按钮icon
        /// </summary>
        public string ButtonIcon { get; set; }

        /// <summary>
        /// 是否分配
        /// </summary>
        public bool IsChecked { get; set; }
    }
}
