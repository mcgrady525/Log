using Log.Entity.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Log.IDao
{
    /// <summary>
    /// 调试日志
    /// </summary>
    public interface ILogsDebugLogDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        bool Insert(TLogsDebugLog item);
    }
}
