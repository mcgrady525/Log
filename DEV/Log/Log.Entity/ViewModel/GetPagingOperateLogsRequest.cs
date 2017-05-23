using Log.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 获取所有操作日志(分页)request
    /// </summary>
    public class GetPagingOperateLogsRequest : PagingBase
    {
        public string SystemCode { get; set; }

        public string Source { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string OperateModule { get; set; }

        public string OperateType { get; set; }

        public DateTime? OperatedTimeStart { get; set; }

        public DateTime? OperatedTimeEnd { get; set; }

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
