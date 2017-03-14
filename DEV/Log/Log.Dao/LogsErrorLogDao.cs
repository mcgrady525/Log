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
using Tracy.Frameworks.Common.Result;
using Log.Entity.ViewModel;
using Tracy.Frameworks.Common.Extends;

namespace Log.Dao
{
    public class LogsErrorLogDao : ILogsErrorLogDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        public bool Insert(TLogsErrorLog item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"INSERT INTO dbo.t_logs_error_log VALUES (@SystemCode ,@Source ,@MachineName ,@IpAddress ,@ProcessId ,@ProcessName ,@ThreadId ,@ThreadName ,@AppdomainName ,@CreatedTime ,@Detail ,@Message);", item);
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

        /// <summary>
        /// 获取所有错误日志(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagingResult<GetPagingErrorLogsResponse> GetPagingErrorLogs(GetPagingErrorLogsRequest request)
        {
            PagingResult<GetPagingErrorLogsResponse> result = null;
            var totalCount = 0;
            var startIndex = (request.PageIndex - 1) * request.PageSize + 1;
            var endIndex = request.PageIndex * request.PageSize;

            //按条件查询，构造where
            //使用DynamicParameters
            var p = new DynamicParameters();
            var sbSqlPaging = new StringBuilder(@"SELECT  ROW_NUMBER() OVER ( ORDER BY errorLogs.id DESC ) AS RowNum ,
                        errorLogs.created_time AS CreatedTime ,
                        errorLogs.system_code AS SystemCode ,
                        errorLogs.ip_address AS IpAddress ,
                        errorLogs.appdomain_name AS AppDomainName ,
                        *
                FROM    dbo.t_logs_error_log AS errorLogs
                WHERE   1 = 1");
            var sbSqlTotal = new StringBuilder(@"SELECT  COUNT(errorLogs.id)
                FROM    dbo.t_logs_error_log AS errorLogs
                WHERE   1 = 1");

            if (!request.SystemCode.IsNullOrEmpty())
            {
                sbSqlPaging.Append(" AND errorLogs.system_code=@SystemCode");
                sbSqlTotal.Append(" AND errorLogs.system_code=@SystemCode");
                p.Add("SystemCode", request.SystemCode, dbType: System.Data.DbType.String);
            }
            if (!request.Source.IsNullOrEmpty())
            {
                sbSqlPaging.Append(" AND errorLogs.source=@Source");
                sbSqlTotal.Append(" AND errorLogs.source=@Source");
                p.Add("Source", request.Source, System.Data.DbType.String);
            }
            if (!request.Message.IsNullOrEmpty())
            {
                sbSqlPaging.Append(" AND errorLogs.message LIKE @Message");
                sbSqlTotal.Append(" AND errorLogs.message LIKE @Message");
                p.Add("Message", "%" + request.Message + "%", System.Data.DbType.String);
            }
            if (request.CreatedTimeStart.HasValue)
            {
                sbSqlPaging.Append(" AND errorLogs.created_time >= @CreatedTimeStart");
                sbSqlTotal.Append(" AND errorLogs.created_time >= @CreatedTimeStart");
                p.Add("CreatedTimeStart", request.CreatedTimeStart.Value, System.Data.DbType.DateTime);
            }
            if (request.CreatedTimeEnd.HasValue)
            {
                sbSqlPaging.Append(" AND errorLogs.created_time <= @CreatedTimeEnd");
                sbSqlTotal.Append(" AND errorLogs.created_time <= @CreatedTimeEnd");
                p.Add("CreatedTimeEnd", request.CreatedTimeEnd.Value, System.Data.DbType.DateTime);
            }

            var sqlPaging = string.Format(@"SELECT  rs.*
                FROM    ( {0}
		                ) AS rs
                WHERE   rs.RowNum BETWEEN @Start AND @End", sbSqlPaging.ToString());
            var sqlStr = string.Format("{0};{1};", sqlPaging, sbSqlTotal.ToString());

            p.Add("Start", startIndex, System.Data.DbType.Int32);
            p.Add("End", endIndex, System.Data.DbType.Int32);

            using (var conn = DapperHelper.CreateConnection())
            {
                var multi = conn.QueryMultiple(sqlStr, p);
                var query1 = multi.Read<GetPagingErrorLogsResponse>();
                var query2 = multi.Read<int>();
                totalCount = query2.First();

                result = new PagingResult<GetPagingErrorLogsResponse>(totalCount, request.PageIndex, request.PageSize, query1);
            }

            return result;
        }

        /// <summary>
        /// 依id查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public TLogsErrorLog GetById(int id)
        {
            TLogsErrorLog result = null;
            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TLogsErrorLog>(@"SELECT  errorLogs.system_code AS SystemCode ,
                            errorLogs.machine_name AS MachineName ,
                            errorLogs.ip_address AS IpAddress ,
                            errorLogs.process_id AS ProcessId ,
                            errorLogs.process_name AS ProcessName ,
                            errorLogs.thread_id AS ThreadId ,
                            errorLogs.thread_name AS ThreadName ,
                            errorLogs.appdomain_name AS AppdomainName ,
                            errorLogs.created_time AS CreatedTime ,
                            *
                    FROM dbo.t_logs_error_log AS errorLogs
                    WHERE   errorLogs.id = @Id;", new { @Id = id }).FirstOrDefault();
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
            using (var conn = DapperHelper.CreateConnection())
            {
                var query = conn.Query<TLogsErrorLogTip>(@"SELECT  errorLogTips.system_code AS SystemCode ,
                            *
                    FROM    dbo.t_logs_error_log_tip AS errorLogTips
                    ORDER BY errorLogTips.system_code ,
                            errorLogTips.source;").ToList();
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
