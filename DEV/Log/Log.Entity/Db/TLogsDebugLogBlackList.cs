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
		/// client_ip
		/// </summary>
		[DataMember]
		[Column("client_ip")] 
		public string ClientIp { get; set; }
		
		/// <summary>
		/// appdomain_name
		/// </summary>
		[DataMember]
		[Column("appdomain_name")] 
		public string AppdomainName { get; set; }
		
		/// <summary>
		/// message
		/// </summary>
		[DataMember]
		[Column("message")] 
		public string Message { get; set; }
		
		/// <summary>
		/// is_regex
		/// </summary>
		[DataMember]
		[Column("is_regex")] 
		public bool? IsRegex { get; set; }
		
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