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
using Tracy.Frameworks.Common.Extends;
using System.Data;

namespace Log.Dao
{
    /// <summary>
    /// 调试日志
    /// </summary>
    public class LogsDebugLogDao : ILogsDebugLogDao
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

            //按条件查询，构造where
            //使用DynamicParameters
            var p = new DynamicParameters();
            var sbSqlPaging = new StringBuilder(@"SELECT  ROW_NUMBER() OVER ( ORDER BY debugLogs.id DESC ) AS RowNum ,
                                        debugLogs.created_time AS CreatedTime ,
                                        debugLogs.system_code AS SystemCode ,
                                        debugLogs.ip_address AS IpAddress ,
                                        debugLogs.appdomain_name AS AppDomainName ,
                                        *
                                FROM    dbo.t_logs_debug_log AS debugLogs
                                WHERE   1 = 1");
            var sbSqlTotal = new StringBuilder(@"SELECT  COUNT(debugLogs.id)
                                FROM    dbo.t_logs_debug_log AS debugLogs
                                WHERE   1 = 1");

            if (!request.SystemCode.IsNullOrEmpty())
            {
                sbSqlPaging.Append(" AND debugLogs.system_code=@SystemCode");
                sbSqlTotal.Append(" AND debugLogs.system_code=@SystemCode");
                p.Add("SystemCode", request.SystemCode, dbType: System.Data.DbType.String);
            }
            if (!request.Source.IsNullOrEmpty())
            {
                sbSqlPaging.Append(" AND debugLogs.source=@Source");
                sbSqlTotal.Append(" AND debugLogs.source=@Source");
                p.Add("Source", request.Source, System.Data.DbType.String);
            }
            if (!request.Message.IsNullOrEmpty())
            {
                sbSqlPaging.Append(" AND debugLogs.message LIKE @Message");
                sbSqlTotal.Append(" AND debugLogs.message LIKE @Message");
                p.Add("Message", "%" + request.Message + "%", System.Data.DbType.String);
            }
            if (request.CreatedTimeStart.HasValue)
            {
                sbSqlPaging.Append(" AND debugLogs.created_time >= @CreatedTimeStart");
                sbSqlTotal.Append(" AND debugLogs.created_time >= @CreatedTimeStart");
                p.Add("CreatedTimeStart", request.CreatedTimeStart.Value, System.Data.DbType.DateTime);
            }
            if (request.CreatedTimeEnd.HasValue)
            {
                sbSqlPaging.Append(" AND debugLogs.created_time <= @CreatedTimeEnd");
                sbSqlTotal.Append(" AND debugLogs.created_time <= @CreatedTimeEnd");
                p.Add("CreatedTimeEnd", request.CreatedTimeEnd.Value, System.Data.DbType.DateTime);
            }

            var sqlPaging = string.Format(@"SELECT  rs.*
                FROM    ( {0}
		                ) AS rs
                WHERE   rs.RowNum BETWEEN @Start AND @End", sbSqlPaging.ToString());
            var sqlStr = string.Format("{0};{1};", sqlPaging, sbSqlTotal.ToString());

            p.Add("Start", request.PageIndex, System.Data.DbType.Int32);
            p.Add("End", request.PageSize, System.Data.DbType.Int32);

            using (var conn = DapperHelper.CreateConnection())
            {
                var multi = conn.QueryMultiple(sqlStr, p);
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
            using (var conn = DapperHelper.CreateConnection())
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

        /// <summary>
        /// 刷新调试日志的智能提示
        /// </summary>
        /// <returns></returns>
        public bool RefreshDebugLogTip()
        {
            var result = false;
            using (var conn = DapperHelper.CreateConnection())
            {
                var p = new DynamicParameters();
                p.Add("IsSuccess", dbType: System.Data.DbType.Int32, direction: ParameterDirection.ReturnValue);

                conn.Execute("usp_RefreshDebugLogTip", p, commandType: CommandType.StoredProcedure);

                var isSuccess = p.Get<int>("IsSuccess");
                result = isSuccess == 1 ? true : false;
            }

            return result;
        }

        /// <summary>
        /// 获取智能提示数据
        /// </summary>
        /// <returns></returns>
        public Tuple<List<string>, List<string>> GetAutoCompleteData()
        {
            var systemCodes = new List<string>();
            var sources = new List<string>();
            using (var conn= DapperHelper.CreateConnection())
            {
                var query = conn.Query<TLogsDebugLogTip>(@"SELECT debugLogTips.system_code AS SystemCode, * FROM dbo.t_logs_debug_log_tip AS debugLogTips
                    ORDER BY debugLogTips.system_code, debugLogTips.source;").ToList();
                if (query.HasValue())
                {
                    systemCodes = query.Select(p => p.SystemCode).Distinct().ToList();
                    sources = query.Select(p => p.Source).Distinct().ToList();
                }
            }

            return new Tuple<List<string>, List<string>>(systemCodes, sources);
        }
    }
}
