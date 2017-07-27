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
using Tracy.Frameworks.Common.Result;
using Log.Entity.ViewModel;
using Tracy.Frameworks.Common.Extends;

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
                var effectRows = conn.Execute(@"INSERT INTO dbo.t_logs_xml_log VALUES (@SystemCode ,@Source ,@MachineName ,@IpAddress ,@ProcessId ,@ProcessName ,@ThreadId ,@ThreadName ,@AppdomainName ,@ClassName ,@MethodName ,@Remark ,@CreatedTime,@Rq ,@Rs, @ClientIp, @MethodCname);", item);
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

        /// <summary>
        /// 获取所有日志(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagingResult<GetPagingXmlLogsResponse> GetPagingXmlLogs(GetPagingXmlLogsRequest request)
        {
            PagingResult<GetPagingXmlLogsResponse> result = null;
            var totalCount = 0;
            var startIndex = (request.PageIndex - 1) * request.PageSize + 1;
            var endIndex = request.PageIndex * request.PageSize;

            //按条件查询，构造where
            //使用DynamicParameters
            var p = new DynamicParameters();
            var sbSqlPaging = new StringBuilder(@"SELECT  ROW_NUMBER() OVER ( ORDER BY xmlLogs.id DESC ) AS RowNum ,
                        xmlLogs.created_time AS CreatedTime ,
                        xmlLogs.system_code AS SystemCode ,
                        xmlLogs.class_name AS ClassName ,
                        xmlLogs.method_name AS MethodName ,
                        xmlLogs.method_cname AS MethodCName,
                        xmlLogs.ip_address AS IpAddress ,
                        xmlLogs.client_ip AS ClientIp,
                        xmlLogs.appdomain_name AS AppDomainName ,
                        *
                FROM    dbo.t_logs_xml_log(NOLOCK) AS xmlLogs
                WHERE   1 = 1");
            var sbSqlTotal = new StringBuilder(@"SELECT  COUNT(xmlLogs.id)
                FROM    dbo.t_logs_xml_log(NOLOCK) AS xmlLogs
                WHERE   1 = 1");

            if (!request.SystemCode.IsNullOrEmpty())
            {
                sbSqlPaging.Append(" AND xmlLogs.system_code=@SystemCode");
                sbSqlTotal.Append(" AND xmlLogs.system_code=@SystemCode");
                p.Add("SystemCode", request.SystemCode, dbType: System.Data.DbType.String);
            }
            if (!request.Source.IsNullOrEmpty())
            {
                sbSqlPaging.Append(" AND xmlLogs.source=@Source");
                sbSqlTotal.Append(" AND xmlLogs.source=@Source");
                p.Add("Source", request.Source, System.Data.DbType.String);
            }
            if (!request.ClassName.IsNullOrEmpty())
            {
                sbSqlPaging.Append(" AND xmlLogs.class_name=@ClassName");
                sbSqlTotal.Append(" AND xmlLogs.class_name=@ClassName");
                p.Add("ClassName", request.ClassName, System.Data.DbType.String);
            }
            if (!request.MethodName.IsNullOrEmpty())
            {
                sbSqlPaging.Append(" AND xmlLogs.method_name=@MethodName");
                sbSqlTotal.Append(" AND xmlLogs.method_name=@MethodName");
                p.Add("MethodName", request.MethodName, System.Data.DbType.String);
            }
            if (!request.MethodCName.IsNullOrEmpty())
            {
                sbSqlPaging.Append(" AND xmlLogs.method_cname=@MethodCName");
                sbSqlTotal.Append(" AND xmlLogs.method_cname=@MethodCName");
                p.Add("MethodCName", request.MethodCName, System.Data.DbType.String);
            }
            if (request.CreatedTimeStart.HasValue)
            {
                sbSqlPaging.Append(" AND xmlLogs.created_time >= @CreatedTimeStart");
                sbSqlTotal.Append(" AND xmlLogs.created_time >= @CreatedTimeStart");
                p.Add("CreatedTimeStart", request.CreatedTimeStart.Value, System.Data.DbType.DateTime);
            }
            if (request.CreatedTimeEnd.HasValue)
            {
                sbSqlPaging.Append(" AND xmlLogs.created_time <= @CreatedTimeEnd");
                sbSqlTotal.Append(" AND xmlLogs.created_time <= @CreatedTimeEnd");
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
                var query1 = multi.Read<GetPagingXmlLogsResponse>();
                var query2 = multi.Read<int>();
                totalCount = query2.First();

                result = new PagingResult<GetPagingXmlLogsResponse>(totalCount, request.PageIndex, request.PageSize, query1);
            }

            return result;
        }

        /// <summary>
        /// 获取智能提示数据源
        /// </summary>
        /// <returns></returns>
        public Tuple<List<string>, List<string>, List<string>, List<string>, List<string>> GetAutoCompleteData()
        {
            var systemCodes = new List<string>();
            var sources = new List<string>();
            var classNames = new List<string>();
            var methodNames = new List<string>();
            var methodCNames = new List<string>();

            using (var conn = DapperHelper.CreateConnection())
            {
                var query = conn.Query<TLogsXmlLogTip>(@"SELECT  xmlLogTips.system_code AS SystemCode ,
                            xmlLogTips.class_name AS ClassName ,
                            xmlLogTips.method_name AS MethodName ,
                            xmlLogTips.method_cname AS MethodCName ,
                            *
                    FROM    dbo.t_logs_xml_log_tip AS xmlLogTips
                    ORDER BY xmlLogTips.system_code ,
                            xmlLogTips.source ,
                            xmlLogTips.class_name ,
                            xmlLogTips.method_name,
                            xmlLogTips.method_cname;").ToList();
                systemCodes = query.Select(p => p.SystemCode).Distinct().ToList();
                sources = query.Select(p => p.Source).Distinct().ToList();
                classNames = query.Select(p => p.ClassName).Distinct().ToList();
                methodNames = query.Select(p => p.MethodName).Distinct().ToList();
                methodCNames = query.Select(p => p.MethodCName).Distinct().ToList();
            }

            return new Tuple<List<string>, List<string>, List<string>, List<string>, List<string>>(systemCodes, sources, classNames, methodNames, methodCNames);
        }

        /// <summary>
        /// 依id查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public TLogsXmlLog GetById(long id)
        {
            TLogsXmlLog xmlLog = null;

            using (var conn = DapperHelper.CreateConnection())
            {
                xmlLog = conn.Query<TLogsXmlLog>(@"SELECT  xmlLogs.system_code AS SystemCode ,
                        xmlLogs.machine_name AS MachineName ,
                        xmlLogs.ip_address AS IpAddress ,
                        xmlLogs.client_ip AS ClientIp,
                        xmlLogs.process_id AS ProcessId ,
                        xmlLogs.process_name AS ProcessName ,
                        xmlLogs.thread_id AS ThreadId ,
                        xmlLogs.thread_name AS ThreadName ,
                        xmlLogs.appdomain_name AS AppdomainName ,
                        xmlLogs.class_name AS ClassName ,
                        xmlLogs.method_name AS MethodName ,
                        xmlLogs.method_cname AS MethodCname,
                        xmlLogs.created_time AS CreatedTime ,
                        *
                FROM    dbo.t_logs_xml_log AS xmlLogs
                WHERE   xmlLogs.id = @Id;", new { @Id = id }).FirstOrDefault();
            }

            return xmlLog;
        }
    }
}
