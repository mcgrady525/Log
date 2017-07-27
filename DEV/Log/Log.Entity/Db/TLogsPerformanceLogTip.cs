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
	[Table("dbo.t_logs_performance_log_tip")]
	public partial class TLogsPerformanceLogTip
	{
		public TLogsPerformanceLogTip()
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
		/// class_name
		/// </summary>
		[DataMember]
		[Column("class_name")] 
		public string ClassName { get; set; }
		
		/// <summary>
		/// method_name
		/// </summary>
		[DataMember]
		[Column("method_name")] 
		public string MethodName { get; set; }
		
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

        /// <summary>
        /// method_cname
        /// </summary>
        [DataMember]
        [Column("method_cname")]
        public string MethodCName { get; set; }
		
	}
}