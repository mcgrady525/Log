using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.IDao;
using Log.Entity.Db;
using Log.Common.Helper;
using Dapper;

namespace Log.Dao
{
    /// <summary>
    /// 调试日志
    /// </summary>
    public class LogsDebugLogDao: ILogsDebugLogDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        public bool Insert(TLogsDebugLog item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"INSERT INTO dbo.t_logs_debug_log VALUES (@SystemCode ,@Source ,@MachineName ,@IpAddress ,@ProcessId ,@ProcessName ,@ThreadId,@ThreadName ,@AppdomainName ,@Message ,@CreatedTime ,@Detail);", item);
                if (effectRows > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
