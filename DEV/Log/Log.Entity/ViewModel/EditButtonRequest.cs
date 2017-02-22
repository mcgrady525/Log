using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 修改按钮request
    /// </summary>
    public class EditButtonRequest
    {
        /// <summary>
        /// 要修改的按钮id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 原按钮名称
        /// </summary>
        public string OriginalName { get; set; }

        /// <summary>
        /// 新的按钮名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 新的按钮icon
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 新的按钮sort
        /// </summary>
        public int Sort { get; set; }

    }
}
