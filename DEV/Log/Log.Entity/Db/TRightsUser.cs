using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Log.Entity.Attributes;

/// <summary>
/// 
/// </summary>
namespace Log.Entity.Db
{
	[Serializable]
	[DataContract(IsReference = true)]
	[Table("dbo.t_rights_user")]
	public partial class TRightsUser
	{
		public TRightsUser()
		{
			
		}

        #region 基本属性
        /// <summary>
        /// id
        /// </summary>
        [DataMember]
        [Column("id", ColumnCategory = Category.IdentityKey)]
        public int Id { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [DataMember]
        [Column("user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DataMember]
        [Column("password")]
        public string Password { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [DataMember]
        [Column("user_name")]
        public string UserName { get; set; }

        /// <summary>
        /// 是否首次登陆改密
        /// </summary>
        [DataMember]
        [Column("is_change_pwd")]
        public bool? IsChangePwd { get; set; }

        /// <summary>
        /// 是否启用标识
        /// </summary>
        [DataMember]
        [Column("enable_flag")]
        public bool? EnableFlag { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        [DataMember]
        [Column("created_by")]
        public int CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        [Column("created_time")]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 最后修改人id
        /// </summary>
        [DataMember]
        [Column("last_updated_by")]
        public int? LastUpdatedBy { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DataMember]
        [Column("last_updated_time")]
        public DateTime? LastUpdatedTime { get; set; } 
        #endregion

        #region 导航属性

        /// <summary>
        /// 用户所属部门,可能多个
        /// </summary>
        public TRightsOrganization Organization { get; set; }

        /// <summary>
        /// 用户拥有的角色,可能多个
        /// </summary>
        public TRightsRole Role { get; set; }

        #endregion

    }
}