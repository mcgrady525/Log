using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Log.Entity.Rights
{
    /// <summary>
    /// 首页-我的信息
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class GetMyInfoResponse
    {
        /// <summary>
        /// 登录id
        /// </summary>
        [DataMember]
        public string UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public string CreatedTime { get; set; }

        /// <summary>
        /// 拥有角色,以','分隔
        /// </summary>
        [DataMember]
        public string RolesName { get; set; }

        /// <summary>
        /// 所属部门,以','分隔
        /// </summary>
        [DataMember]
        public string DepartmentsName { get; set; }
    }
}
