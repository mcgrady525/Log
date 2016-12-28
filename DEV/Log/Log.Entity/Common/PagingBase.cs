using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.Common
{
    [Serializable]
    public class PagingBase
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 默认1页20条记录
        /// </summary>
        public PagingBase()
        {
            PageIndex = 1;
            PageSize = 20;
        }

    }
}
