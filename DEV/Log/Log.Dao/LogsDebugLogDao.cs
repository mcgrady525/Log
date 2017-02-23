using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.IDao;
using Log.Entity.Db;
using Log.Common.Helper;
using Dapper;
using Tracy.Frameworks.Common.Result;
using Log.Entity.ViewModel;

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

        /// <summary>
        /// 获取所有调试日志(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagingResult<GetPagingDebugLogsResponse> GetPagingDebugLogs(GetPagingDebugLogsRequest request)
        {
            PagingResult<GetPagingDebugLogsResponse> result = null;
            var totalCount = 0;
            var startIndex = (request.PageIndex - 1) * request.PageSize + 1;
            var endIndex = request.PageIndex * request.PageSize;

            using (var conn = DapperHelper.CreateConnection())
            {
                var multi = conn.QueryMultiple(@"--获取所有调试日志(分页)
                    SELECT  rs.*
                    FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY debugLogs.id DESC ) AS RowNum ,
                                        debugLogs.created_time AS CreatedTime ,
                                        debugLogs.system_code AS SystemCode ,
                                        debugLogs.ip_address AS IpAddress ,
                                        debugLogs.appdomain_name AS AppDomainName ,
                                        *
                              FROM      dbo.t_logs_debug_log AS debugLogs
                            ) AS rs
                    WHERE   rs.RowNum BETWEEN @Start AND @End;

                    --获取所有调试日志total
                    SELECT  COUNT(debugLogs.id)
                    FROM    dbo.t_logs_debug_log AS debugLogs;", new { @Start = startIndex, @End = endIndex });
                var query1 = multi.Read<GetPagingDebugLogsResponse>();
                var query2 = multi.Read<int>();
                totalCount = query2.First();

                result = new PagingResult<GetPagingDebugLogsResponse>(totalCount, request.PageIndex, request.PageSize, query1);
            }

            return result;
        }

        /// <summary>
        /// 依id查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public TLogsDebugLog GetById(int id)
        {
            TLogsDebugLog result = null;
            using (var conn= DapperHelper.CreateConnection())
            {
                result = conn.Query<TLogsDebugLog>(@"SELECT  debugLogs.system_code AS SystemCode ,
                            debugLogs.machine_name AS MachineName ,
                            debugLogs.ip_address AS IpAddress ,
                            debugLogs.process_id AS ProcessId ,
                            debugLogs.process_name AS ProcessName ,
                            debugLogs.thread_id AS ThreadId ,
                            debugLogs.thread_name AS ThreadName ,
                            debugLogs.appdomain_name AS AppdomainName ,
                            debugLogs.created_time AS CreatedTime ,
                            *
                    FROM    dbo.t_logs_debug_log AS debugLogs
                    WHERE   debugLogs.id = @Id;", new { @Id = id }).FirstOrDefault();
            }

            return result;
        }
    }
}
