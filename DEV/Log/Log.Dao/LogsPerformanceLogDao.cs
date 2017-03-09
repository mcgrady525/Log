using Log.Entity.Db;
using Log.IDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.Common.Helper;
using Dapper;
using Tracy.Frameworks.Common.Result;
using Log.Entity.ViewModel;
using Tracy.Frameworks.Common.Extends;
using System.Data;

namespace Log.Dao
{
    /// <summary>
    /// 性能日志(perf log)Dao
    /// </summary>
    public class LogsPerformanceLogDao : ILogsPerformanceLogDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        public bool Insert(TLogsPerformanceLog item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"INSERT  INTO dbo.t_logs_performance_log
                        ( system_code ,
                          source ,
                          machine_name ,
                          ip_address ,
                          process_id ,
                          process_name ,
                          thread_id ,
                          thread_name ,
                          class_name ,
                          method_name ,
                          duration ,
                          remark ,
                          created_time
                        )
                VALUES  ( @SystemCode ,
                          @Source ,
                          @MachineName ,
                          @IpAddress ,
                          @ProcessId ,
                          @ProcessName ,
                          @ThreadId ,
                          @ThreadName ,
                          @ClassName ,
                          @MethodName ,
                          @Duration ,
                          @Remark ,
                          @CreatedTime
                        );", item);
                if (effectRows > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 获取所有日志(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagingResult<GetPagingPerformanceLogsResponse> GetPagingPerformanceLogs(GetPagingPerformanceLogsRequest request)
        {
            PagingResult<GetPagingPerformanceLogsResponse> result = null;
            var totalCount = 0;
            var startIndex = (request.PageIndex - 1) * request.PageSize + 1;
            var endIndex = request.PageIndex * request.PageSize;

            //按条件查询，构造where
            //使用DynamicParameters
            var p = new DynamicParameters();
            var sbSqlPaging = new StringBuilder(@"SELECT  ROW_NUMBER() OVER ( ORDER BY perfLogs.id DESC ) AS RowNum ,
                        perfLogs.created_time AS CreatedTime ,
                        perfLogs.system_code AS SystemCode ,
                        perfLogs.class_name AS ClassName ,
                        perfLogs.method_name AS MethodName ,
                        perfLogs.machine_name AS MachineName ,
                        perfLogs.ip_address AS IpAddress ,
                        *
                FROM    dbo.t_logs_performance_log (NOLOCK) AS perfLogs
                WHERE   1 = 1");
            var sbSqlTotal = new StringBuilder(@"SELECT  COUNT(perfLogs.id)
                FROM    dbo.t_logs_performance_log (NOLOCK) AS perfLogs
                WHERE   1 = 1");

            if (!request.SystemCode.IsNullOrEmpty())
            {
                sbSqlPaging.Append(" AND perfLogs.system_code=@SystemCode");
                sbSqlTotal.Append(" AND perfLogs.system_code=@SystemCode");
                p.Add("SystemCode", request.SystemCode, dbType: System.Data.DbType.String);
            }
            if (!request.Source.IsNullOrEmpty())
            {
                sbSqlPaging.Append(" AND perfLogs.source=@Source");
                sbSqlTotal.Append(" AND perfLogs.source=@Source");
                p.Add("Source", request.Source, System.Data.DbType.String);
            }
            if (!request.ClassName.IsNullOrEmpty())
            {
                sbSqlPaging.Append(" AND perfLogs.class_name=@ClassName");
                sbSqlTotal.Append(" AND perfLogs.class_name=@ClassName");
                p.Add("ClassName", request.ClassName, System.Data.DbType.String);
            }
            if (!request.MethodName.IsNullOrEmpty())
            {
                sbSqlPaging.Append(" AND perfLogs.method_name=@MethodName");
                sbSqlTotal.Append(" AND perfLogs.method_name=@MethodName");
                p.Add("MethodName", request.MethodName, System.Data.DbType.String);
            }
            if (request.CreatedTimeStart.HasValue)
            {
                sbSqlPaging.Append(" AND perfLogs.created_time >= @CreatedTimeStart");
                sbSqlTotal.Append(" AND perfLogs.created_time >= @CreatedTimeStart");
                p.Add("CreatedTimeStart", request.CreatedTimeStart.Value, System.Data.DbType.DateTime);
            }
            if (request.CreatedTimeEnd.HasValue)
            {
                sbSqlPaging.Append(" AND perfLogs.created_time <= @CreatedTimeEnd");
                sbSqlTotal.Append(" AND perfLogs.created_time <= @CreatedTimeEnd");
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
                var query1 = multi.Read<GetPagingPerformanceLogsResponse>();
                var query2 = multi.Read<int>();
                totalCount = query2.First();

                result = new PagingResult<GetPagingPerformanceLogsResponse>(totalCount, request.PageIndex, request.PageSize, query1);
            }

            return result;
        }

        /// <summary>
        /// 刷新智能提示
        /// </summary>
        /// <returns></returns>
        public bool RefreshPerfLogTip()
        {
            var result = false;
            using (var conn = DapperHelper.CreateConnection())
            {
                var p = new DynamicParameters();
                p.Add("IsSuccess", dbType: System.Data.DbType.Int32, direction: ParameterDirection.ReturnValue);

                conn.Execute("usp_RefreshPerformanceLogTip", p, commandType: CommandType.StoredProcedure);

                var isSuccess = p.Get<int>("IsSuccess");
                result = isSuccess == 1 ? true : false;
            }

            return result;
        }

        /// <summary>
        /// 获取智能提示数据源
        /// </summary>
        /// <returns></returns>
        public Tuple<List<string>, List<string>, List<string>, List<string>> GetAutoCompleteData()
        {
            var systemCodes = new List<string>();
            var sources = new List<string>();
            var classNames = new List<string>();
            var methodNames = new List<string>();

            using (var conn = DapperHelper.CreateConnection())
            {
                var query = conn.Query<TLogsXmlLogTip>(@"SELECT  perfLogTips.system_code AS SystemCode ,
                            perfLogTips.class_name AS ClassName ,
                            perfLogTips.method_name AS MethodName ,
                            *
                    FROM    dbo.t_logs_performance_log_tip (NOLOCK) AS perfLogTips
                    ORDER BY perfLogTips.system_code ,
                            perfLogTips.source ,
                            perfLogTips.class_name ,
                            perfLogTips.method_name;").ToList();
                systemCodes = query.Select(p => p.SystemCode).Distinct().ToList();
                sources = query.Select(p => p.Source).Distinct().ToList();
                classNames = query.Select(p => p.ClassName).Distinct().ToList();
                methodNames = query.Select(p => p.MethodName).Distinct().ToList();
            }

            return new Tuple<List<string>, List<string>, List<string>, List<string>>(systemCodes, sources, classNames, methodNames);
        }

        /// <summary>
        /// 依据id获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TLogsPerformanceLog GetById(long id)
        {
            TLogsPerformanceLog perfLog = null;

            using (var conn= DapperHelper.CreateConnection())
            {
                perfLog = conn.Query<TLogsPerformanceLog>(@"SELECT  perfLogs.system_code AS SystemCode ,
                        perfLogs.machine_name AS MachineName ,
                        perfLogs.ip_address AS IpAddress ,
                        perfLogs.process_id AS ProcessId ,
                        perfLogs.process_name AS ProcessName ,
                        perfLogs.thread_id AS ThreadId ,
                        perfLogs.thread_name AS ThreadName ,
                        perfLogs.class_name AS ClassName ,
                        perfLogs.method_name AS MethodName ,
                        perfLogs.created_time AS CreatedTime ,
                        *
                FROM    dbo.t_logs_performance_log (NOLOCK) AS perfLogs
                WHERE   perfLogs.id = @Id;", new { @Id = id }).FirstOrDefault();
            }

            return perfLog;
        }
    }
}
