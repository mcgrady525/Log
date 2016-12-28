using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 查询用户列表(分页)返回结果
    /// </summary>
    [Serializable]
    public class GetPagingUsersResponse
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 登录id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool EnableFlag { get; set; }

        /// <summary>
        /// 是否改密
        /// </summary>
        public bool IsChangePwd { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 上次修改时间
        /// </summary>
        public DateTime? LastUpdatedTime { get; set; }

        /// <summary>
        /// 用户所属机构id，多个机构以','分隔
        /// </summary>
        public string UserOrgIds { get; set; }

        public string UserOrgNames { get; set; }

        /// <summary>
        /// 用户拥有角色id，多个角色以','分隔
        /// </summary>
        public string UserRoleIds { get; set; }

        public string UserRoleNames { get; set; }
    }
}
