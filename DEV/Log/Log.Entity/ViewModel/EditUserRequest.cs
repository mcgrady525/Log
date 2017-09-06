using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 修改用户request
    /// </summary>
    public class EditUserRequest
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 原用户id
        /// </summary>
        public string OriginalUserId { get; set; }

        /// <summary>
        /// 新的用户id
        /// </summary>
        public string NewUserId { get; set; }

        /// <summary>
        /// 新的用户名
        /// </summary>
        public string NewUserName { get; set; }

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
