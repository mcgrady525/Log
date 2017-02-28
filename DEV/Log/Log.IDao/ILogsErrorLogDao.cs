using Log.Entity.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.IDao
{
    /// <summary>
    /// error log
    /// </summary>
    public interface ILogsErrorLogDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        bool Insert(TLogsErrorLog item);

        /// <summary>
        /// 刷新错误日志的智能提示
        /// </summary>
        /// <returns></returns>
        bool RefreshErrorLogTip();
    }
}
