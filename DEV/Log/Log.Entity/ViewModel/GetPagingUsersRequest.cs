using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Log.Entity.Common;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 获取用户列表(分页)
    /// </summary>
    public class GetPagingUsersRequest : PagingBase
    {
        /// <summary>
        /// 机构id，0表示查询所有，且包含所有子机构
        /// </summary>
        public int OrgId { get; set; }
    }
}
