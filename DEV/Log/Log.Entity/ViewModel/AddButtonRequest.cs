using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 添加按钮request
    /// </summary>
    public class AddButtonRequest
    {
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 标识码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// icon
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
