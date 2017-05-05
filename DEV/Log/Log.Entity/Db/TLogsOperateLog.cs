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
	[Table("dbo.t_logs_operate_log")]
	public partial class TLogsOperateLog
	{
		public TLogsOperateLog()
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
		/// operated_time
		/// </summary>
		[DataMember]
		[Column("operated_time")] 
		public DateTime? OperatedTime { get; set; }
		
		/// <summary>
		/// user_id
		/// </summary>
		[DataMember]
		[Column("user_id")] 
		public string UserId { get; set; }
		
		/// <summary>
		/// user_name
		/// </summary>
		[DataMember]
		[Column("user_name")] 
		public string UserName { get; set; }
		
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
		/// modify_before
		/// </summary>
		[DataMember]
		[Column("modify_before")] 
		public byte[] ModifyBefore { get; set; }
		
		/// <summary>
		/// modify_after
		/// </summary>
		[DataMember]
		[Column("modify_after")] 
		public byte[] ModifyAfter { get; set; }
		
		/// <summary>
		/// created_time
		/// </summary>
		[DataMember]
		[Column("created_time")] 
		public DateTime? CreatedTime { get; set; }
		
		/// <summary>
		/// client_ip
		/// </summary>
		[DataMember]
		[Column("client_ip")] 
		public string ClientIp { get; set; }
		
	}
}