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
	[Table("dbo.t_rights_organization")]
	public partial class TRightsOrganization
	{
		public TRightsOrganization()
		{
			
		}

		/// <summary>
		/// id
		/// </summary>
		[DataMember]
		[Column("id", ColumnCategory=Category.IdentityKey)]
		public int Id { get; set; }
		
		/// <summary>
		/// 机构名称
		/// </summary>
		[DataMember]
		[Column("name")] 
		public string Name { get; set; }
		
		/// <summary>
		/// 上级机构id
		/// </summary>
		[DataMember]
		[Column("parent_id")] 
		public int ParentId { get; set; }
		
		/// <summary>
		/// 机构编码
		/// </summary>
		[DataMember]
		[Column("code")] 
		public string Code { get; set; }
		
		/// <summary>
		/// 机构类型，总公司，分公司，部门等。
		/// </summary>
		[DataMember]
		[Column("organization_type")] 
		public byte? OrganizationType { get; set; }
		
		/// <summary>
		/// 排序
		/// </summary>
		[DataMember]
		[Column("sort")] 
		public int? Sort { get; set; }
		
		/// <summary>
		/// 是否启用标识，Y表示启用N表示禁用，默认启用。
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
		
	}
}