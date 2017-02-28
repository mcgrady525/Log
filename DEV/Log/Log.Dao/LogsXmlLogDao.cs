using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.IDao;
using Log.Entity.Db;
using Log.Common.Helper;
using Dapper;
using System.Data;

namespace Log.Dao
{
    public class LogsXmlLogDao : ILogsXmlLogDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        public bool Insert(TLogsXmlLog item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"INSERT INTO dbo.t_logs_xml_log VALUES (@SystemCode ,@Source ,@MachineName ,@IpAddress ,@ProcessId ,@ProcessName ,@ThreadId ,@ThreadName ,@AppdomainName ,@ClassName ,@MethodName ,@Remark ,@CreatedTime,@Rq ,@Rs);", item);
                if (effectRows > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 刷新xml日志的智能提示
        /// </summary>
        /// <returns></returns>
        public bool RefreshXmlLogTip()
        {
            var result = false;
            using (var conn = DapperHelper.CreateConnection())
            {
                var p = new DynamicParameters();
                p.Add("IsSuccess", dbType: System.Data.DbType.Int32, direction: ParameterDirection.ReturnValue);

                conn.Execute("usp_RefreshXmlLogTip", p, commandType: CommandType.StoredProcedure);

                var isSuccess = p.Get<int>("IsSuccess");
                result = isSuccess == 1 ? true : false;
            }

            return result;
        }
    }
}
