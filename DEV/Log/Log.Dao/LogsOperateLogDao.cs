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
    /// OperateLog操作日志dao
    /// </summary>
    public class LogsOperateLogDao : ILogsOperateLogDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        public bool Insert(TLogsOperateLog item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectedRows = conn.Execute(@"INSERT INTO dbo.t_logs_operate_log VALUES  (@SystemCode ,@Source ,@MachineName ,@IpAddress ,@ProcessId ,@ProcessName ,@ThreadId ,@ThreadName ,@AppdomainName ,@OperatedTime ,@UserId ,@UserName ,@OperateModule ,@OperateType ,@ModifyBefore ,@ModifyAfter ,@CreatedTime);", item);
                if (effectedRows > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
