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
	[Table("dbo.t_logs_operate_log_tip")]
	public partial class TLogsOperateLogTip
	{
		public TLogsOperateLogTip()
		{
			
		}

		/// <summary>
		/// id
		/// </summary>
		[DataMember]
		[Column("id", ColumnCategory=Category.IdentityKey)]
		public long Id { get; set; }
		
		/// <summary>
		/// system_code
		/// </summary>
		[DataMember]
		[Column("system_code")] 
		public string SystemCode { get; set; }
		
		/// <summary>
		/// source
		/// </summary>
		[DataMember]
		[Column("source")] 
		public string Source { get; set; }
		
		/// <summary>
		/// operate_module
		/// </summary>
		[DataMember]
		[Column("operate_module")] 
		public string OperateModule { get; set; }
		
		/// <summary>
		/// operate_type
		/// </summary>
		[DataMember]
		[Column("operate_type")] 
		public string OperateType { get; set; }
		
		/// <summary>
		/// created_time
		/// </summary>
		[DataMember]
		[Column("created_time")] 
		public DateTime? CreatedTime { get; set; }
		
		/// <summary>
		/// modified_time
		/// </summary>
		[DataMember]
		[Column("modified_time")] 
		public DateTime? ModifiedTime { get; set; }
		
	}
}