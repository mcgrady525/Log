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
	[Table("dbo.t_logs_error_log")]
	public partial class TLogsErrorLog
	{
		public TLogsErrorLog()
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
		/// machine_name
		/// </summary>
		[DataMember]
		[Column("machine_name")] 
		public string MachineName { get; set; }
		
		/// <summary>
		/// ip_address
		/// </summary>
		[DataMember]
		[Column("ip_address")] 
		public string IpAddress { get; set; }
		
		/// <summary>
		/// process_id
		/// </summary>
		[DataMember]
		[Column("process_id")] 
		public int? ProcessId { get; set; }
		
		/// <summary>
		/// process_name
		/// </summary>
		[DataMember]
		[Column("process_name")] 
		public string ProcessName { get; set; }
		
		/// <summary>
		/// thread_id
		/// </summary>
		[DataMember]
		[Column("thread_id")] 
		public int? ThreadId { get; set; }
		
		/// <summary>
		/// thread_name
		/// </summary>
		[DataMember]
		[Column("thread_name")] 
		public string ThreadName { get; set; }
		
		/// <summary>
		/// appdomain_name
		/// </summary>
		[DataMember]
		[Column("appdomain_name")] 
		public string AppdomainName { get; set; }
		
		/// <summary>
		/// created_time
		/// </summary>
		[DataMember]
		[Column("created_time")] 
		public DateTime? CreatedTime { get; set; }
		
		/// <summary>
		/// detail
		/// </summary>
		[DataMember]
		[Column("detail")] 
		public byte[] Detail { get; set; }
		
		/// <summary>
		/// message
		/// </summary>
		[DataMember]
		[Column("message")] 
		public byte[] Message { get; set; }
		
	}
}