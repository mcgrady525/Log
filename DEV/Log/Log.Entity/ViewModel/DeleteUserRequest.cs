using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 删除用户request
    /// </summary>
    [Serializable]
    public class DeleteUserRequest
    {
        /// <summary>
        /// 待删除的id，多个id以','分隔
        /// </summary>
        public string Ids { get; set; }

    }
}
