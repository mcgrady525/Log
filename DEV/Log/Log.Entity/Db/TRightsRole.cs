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
	[Table("dbo.t_rights_role")]
	public partial class TRightsRole
	{
		public TRightsRole()
		{
			
		}

		/// <summary>
		/// id
		/// </summary>
		[DataMember]
		[Column("id", ColumnCategory=Category.IdentityKey)]
		public int Id { get; set; }
		
		/// <summary>
		/// 角色名称
		/// </summary>
		[DataMember]
		[Column("name")] 
		public string Name { get; set; }
		
		/// <summary>
		/// 角色描述
		/// </summary>
		[DataMember]
		[Column("description")] 
		public string Description { get; set; }
		
		/// <summary>
		/// 所属机构id，角色有所属机构的。
		/// </summary>
		[DataMember]
		[Column("organization_id")] 
		public int OrganizationId { get; set; }
		
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
		/// 最后更新人id
		/// </summary>
		[DataMember]
		[Column("last_updated_by")] 
		public int? LastUpdatedBy { get; set; }
		
		/// <summary>
		/// 最后更新时间
		/// </summary>
		[DataMember]
		[Column("last_updated_time")] 
		public DateTime? LastUpdatedTime { get; set; }
		
	}
}