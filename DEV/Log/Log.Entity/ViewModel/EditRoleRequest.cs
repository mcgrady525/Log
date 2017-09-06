using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 修改角色request
    /// </summary>
    public class EditRoleRequest
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 原角色名
        /// </summary>
        public string OriginalName { get; set; }

        /// <summary>
        /// 新角色名
        /// </summary>
        public string NewName { get; set; }

        /// <summary>
        /// 所属机构id
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }

    }
}
