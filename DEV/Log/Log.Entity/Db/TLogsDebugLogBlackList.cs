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
	[Table("dbo.t_logs_debug_log_black_list")]
	public partial class TLogsDebugLogBlackList
	{
		public TLogsDebugLogBlackList()
		{
			
		}

		/// <summary>
		/// id
		/// </summary>
		[DataMember]
		[Column("id", ColumnCategory=Category.IdentityKey)]
		public long Id { get; set; }
		
		/// <summary>
		/// content
		/// </summary>
		[DataMember]
		[Column("content")] 
		public string Content { get; set; }
		
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