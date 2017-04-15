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
	[Table("dbo.t_logs_xml_log")]
	public partial class TLogsXmlLog
	{
		public TLogsXmlLog()
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
		/// remark
		/// </summary>
		[DataMember]
		[Column("remark")] 
		public string Remark { get; set; }
		
		/// <summary>
		/// created_time
		/// </summary>
		[DataMember]
		[Column("created_time")] 
		public DateTime? CreatedTime { get; set; }
		
		/// <summary>
		/// rq
		/// </summary>
		[DataMember]
		[Column("rq")] 
		public byte[] Rq { get; set; }
		
		/// <summary>
		/// rs
		/// </summary>
		[DataMember]
		[Column("rs")] 
		public byte[] Rs { get; set; }
		
		/// <summary>
		/// client_ip
		/// </summary>
		[DataMember]
		[Column("client_ip")] 
		public string ClientIp { get; set; }
		
		/// <summary>
		/// method_cname
		/// </summary>
		[DataMember]
		[Column("method_cname")] 
		public string MethodCname { get; set; }
		
	}
}