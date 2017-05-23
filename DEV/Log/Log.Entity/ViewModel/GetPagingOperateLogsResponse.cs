using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 获取所有操作日志(分页)response
    /// </summary>
    public class GetPagingOperateLogsResponse
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 系统代码
        /// </summary>
        public string SystemCode { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// IpAddress
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// ClientIp
        /// </summary>
        public string ClientIp { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperatedTime { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// OperateModule
        /// </summary>
        public string OperateModule { get; set; }

        /// <summary>
        /// OperateType
        /// </summary>
        public string OperateType { get; set; }

        /// <summary>
        /// 日志详情url，格式：http://localhost.dev.ssharing.com:8080/LogSite/OperateLog/detail/1
        /// </summary>
        public string DetailUrl { get; set; }

        /// <summary>
        /// 客户id
        /// </summary>
        public long CorpId { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CorpName { get; set; }

        /// <summary>
        /// 备注，方便通过关键字过滤
        /// </summary>
        public string Remark { get; set; }

    }
}
