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
	[Table("dbo.t_rights_user_organization")]
	public partial class TRightsUserOrganization
	{
		public TRightsUserOrganization()
		{
			
		}

		/// <summary>
		/// 主键
		/// </summary>
		[DataMember]
		[Column("id", ColumnCategory=Category.IdentityKey)]
		public int Id { get; set; }
		
		/// <summary>
		/// 用户id
		/// </summary>
		[DataMember]
		[Column("user_id")] 
		public int? UserId { get; set; }
		
		/// <summary>
		/// 机构id
		/// </summary>
		[DataMember]
		[Column("organization_id")] 
		public int? OrganizationId { get; set; }
		
	}
}