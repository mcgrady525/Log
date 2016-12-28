using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 获取所有按钮(分页)
    /// </summary>
    [Serializable]
    public class GetPagingButtonsResponse
    {
        /// <summary>
        /// 按钮id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 按钮名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 按钮标识码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 按钮icon
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 上次修改时间
        /// </summary>
        public DateTime LastUpdatedTime { get; set; }

    }
}
