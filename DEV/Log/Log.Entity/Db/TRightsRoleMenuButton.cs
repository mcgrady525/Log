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
	[Table("dbo.t_rights_role_menu_button")]
	public partial class TRightsRoleMenuButton
	{
		public TRightsRoleMenuButton()
		{
			
		}

		/// <summary>
		/// id
		/// </summary>
		[DataMember]
		[Column("id", ColumnCategory=Category.IdentityKey)]
		public int Id { get; set; }
		
		/// <summary>
		/// 角色id
		/// </summary>
		[DataMember]
		[Column("role_id")] 
		public int? RoleId { get; set; }
		
		/// <summary>
		/// 菜单id
		/// </summary>
		[DataMember]
		[Column("menu_id")] 
		public int? MenuId { get; set; }
		
		/// <summary>
		/// 按钮id
		/// </summary>
		[DataMember]
		[Column("button_id")] 
		public int? ButtonId { get; set; }
		
	}
}