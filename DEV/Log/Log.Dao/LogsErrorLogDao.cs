using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.IDao;
using Log.Entity.Db;
using Dapper;
using Log.Common.Helper;
using System.Data;

namespace Log.Dao
{
    public class LogsErrorLogDao: ILogsErrorLogDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        public bool Insert(TLogsErrorLog item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"INSERT INTO dbo.t_logs_error_log VALUES (@SystemCode ,@Source ,@MachineName ,@IpAddress ,@ProcessId ,@ProcessName ,@ThreadId ,@ThreadName ,@AppdomainName ,@Message ,@CreatedTime ,@Detail);", item);
                if (effectRows > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 刷新错误日志的智能提示
        /// </summary>
        /// <returns></returns>
        public bool RefreshErrorLogTip()
        {
            var result = false;
            using (var conn = DapperHelper.CreateConnection())
            {
                var p = new DynamicParameters();
                p.Add("IsSuccess", dbType: System.Data.DbType.Int32, direction: ParameterDirection.ReturnValue);

                conn.Execute("usp_RefreshErrorLogTip", p, commandType: CommandType.StoredProcedure);

                var isSuccess = p.Get<int>("IsSuccess");
                result = isSuccess == 1 ? true : false;
            }

            return result;
        }
    }
}
