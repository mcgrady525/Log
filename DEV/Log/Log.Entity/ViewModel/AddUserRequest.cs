using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 新增用户request
    /// </summary>
    [Serializable]
    public class AddUserRequest
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户名称
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

    }
}
