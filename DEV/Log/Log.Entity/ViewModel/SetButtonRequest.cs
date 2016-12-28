using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 分配按钮request
    /// </summary>
    [Serializable]
    public class SetButtonRequest
    {
        /// <summary>
        /// 为哪个菜单分配按钮
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// 勾选了哪些按钮
        /// </summary>
        public string buttonIds { get; set; }
    }
}
