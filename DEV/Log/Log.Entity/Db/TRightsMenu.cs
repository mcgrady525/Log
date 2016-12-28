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
	[Table("dbo.t_rights_menu")]
	public partial class TRightsMenu
	{
		public TRightsMenu()
		{
			
		}

		/// <summary>
		/// id
		/// </summary>
		[DataMember]
		[Column("id", ColumnCategory=Category.IdentityKey)]
		public int Id { get; set; }
		
		/// <summary>
		/// 菜单名称
		/// </summary>
		[DataMember]
		[Column("name")] 
		public string Name { get; set; }
		
		/// <summary>
		/// 上级菜单id
		/// </summary>
		[DataMember]
		[Column("parent_id")] 
		public int ParentId { get; set; }
		
		/// <summary>
		/// 菜单编码
		/// </summary>
		[DataMember]
		[Column("code")] 
		public string Code { get; set; }
		
		/// <summary>
		/// 菜单url
		/// </summary>
		[DataMember]
		[Column("url")] 
		public string Url { get; set; }
		
		/// <summary>
		/// 菜单图标
		/// </summary>
		[DataMember]
		[Column("icon")] 
		public string Icon { get; set; }
		
		/// <summary>
		/// 菜单排序
		/// </summary>
		[DataMember]
		[Column("sort")] 
		public int? Sort { get; set; }
		
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