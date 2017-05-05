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

        /// <summary>
        /// 获取所有操作日志(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagingResult<GetPagingOperateLogsResponse> GetPagingOperateLogs(GetPagingOperateLogsRequest request)
        {
            PagingResult<GetPagingOperateLogsResponse> result = null;
            var totalCount = 0;
            var startIndex = (request.PageIndex - 1) * request.PageSize + 1;
            var endIndex = request.PageIndex * request.PageSize;

            //按条件查询，构造where
            //使用DynamicParameters
            var p = new DynamicParameters();
            var sbSqlPaging = new StringBuilder(@"SELECT  ROW_NUMBER() OVER ( ORDER BY operateLogs.id DESC ) AS RowNum ,
                                        operateLogs.created_time AS CreatedTime ,
                                        operateLogs.system_code AS SystemCode ,
                                        operateLogs.ip_address AS IpAddress,
                                        operateLogs.client_ip AS ClientIp,
                                        operateLogs.operated_time AS OperatedTime,
                                        operateLogs.user_id AS UserId,
                                        operateLogs.user_name AS UserName,
                                        operateLogs.operate_module AS OperateModule,
                                        operateLogs.operate_type AS OperateType,
                                        *
                                FROM    dbo.t_logs_operate_log(NOLOCK) AS operateLogs
                                WHERE   1 = 1");
            var sbSqlTotal = new StringBuilder(@"SELECT  COUNT(operateLogs.id)
                                FROM    dbo.t_logs_operate_log(NOLOCK) AS operateLogs
                                WHERE   1 = 1");

            if (!request.SystemCode.IsNullOrEmpty())
            {
                sbSqlPaging.Append(" AND operateLogs.system_code=@SystemCode");
                sbSqlTotal.Append(" AND operateLogs.system_code=@SystemCode");
                p.Add("SystemCode", request.SystemCode, dbType: System.Data.DbType.String);
            }
            if (!request.Source.IsNullOrEmpty())
            {
                sbSqlPaging.Append(" AND operateLogs.source=@Source");
                sbSqlTotal.Append(" AND operateLogs.source=@Source");
                p.Add("Source", request.Source, System.Data.DbType.String);
            }
            if (!request.UserId.IsNullOrEmpty())
            {
                sbSqlPaging.Append(" AND operateLogs.user_id LIKE @UserId");
                sbSqlTotal.Append(" AND operateLogs.user_id LIKE @UserId");
                p.Add("UserId", "%" + request.UserId + "%", System.Data.DbType.String);
            }
            if (!request.UserName.IsNullOrEmpty())
            {
                sbSqlPaging.Append(" AND operateLogs.user_name LIKE @UserName");
                sbSqlTotal.Append(" AND operateLogs.user_name LIKE @UserName");
                p.Add("UserName", "%" + request.UserName + "%", System.Data.DbType.String);
            }
            if (!request.OperateModule.IsNullOrEmpty())
            {
                sbSqlPaging.Append(" AND operateLogs.operate_module=@OperateModule");
                sbSqlTotal.Append(" AND operateLogs.operate_module=@OperateModule");
                p.Add("OperateModule", request.OperateModule, System.Data.DbType.String);
            }
            if (!request.OperateType.IsNullOrEmpty())
            {
                sbSqlPaging.Append(" AND operateLogs.operate_type=@OperateType");
                sbSqlTotal.Append(" AND operateLogs.operate_type=@OperateType");
                p.Add("OperateType", request.OperateType, System.Data.DbType.String);
            }
            if (request.OperatedTimeStart.HasValue)
            {
                sbSqlPaging.Append(" AND operateLogs.operated_time >= @OperatedTimeStart");
                sbSqlTotal.Append(" AND operateLogs.operated_time >= @OperatedTimeStart");
                p.Add("OperatedTimeStart", request.OperatedTimeStart.Value, System.Data.DbType.DateTime);
            }
            if (request.OperatedTimeEnd.HasValue)
            {
                sbSqlPaging.Append(" AND operateLogs.operated_time <= @OperatedTimeEnd");
                sbSqlTotal.Append(" AND operateLogs.operated_time <= @OperatedTimeEnd");
                p.Add("OperatedTimeEnd", request.OperatedTimeEnd.Value, System.Data.DbType.DateTime);
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
                var query1 = multi.Read<GetPagingOperateLogsResponse>();
                var query2 = multi.Read<int>();
                totalCount = query2.First();

                result = new PagingResult<GetPagingOperateLogsResponse>(totalCount, request.PageIndex, request.PageSize, query1);
            }

            return result;
        }

        /// <summary>
        /// 刷新智能提示
        /// </summary>
        /// <returns></returns>
        public bool RefreshOperateLogTip()
        {
            var result = false;
            using (var conn = DapperHelper.CreateConnection())
            {
                var p = new DynamicParameters();
                p.Add("IsSuccess", dbType: System.Data.DbType.Int32, direction: ParameterDirection.ReturnValue);

                conn.Execute("usp_RefreshOperateLogTip", p, commandType: CommandType.StoredProcedure);

                var isSuccess = p.Get<int>("IsSuccess");
                result = isSuccess == 1 ? true : false;
            }

            return result;
        }

        /// <summary>
        /// 获取智能提示数据
        /// </summary>
        /// <returns></returns>
        public Tuple<List<string>, List<string>, List<string>, List<string>> GetAutoCompleteData()
        {
            var systemCodes = new List<string>();
            var sources = new List<string>();
            var operateModules = new List<string>();
            var operateTypes = new List<string>();

            using (var conn = DapperHelper.CreateConnection())
            {
                var query = conn.Query<TLogsOperateLogTip>(@"SELECT  operateLogTips.system_code AS SystemCode ,
		                                operateLogTips.source AS Source,
                                        operateLogTips.operate_module AS OperateModule ,
                                        operateLogTips.operate_type AS OperateType
                                FROM    dbo.t_logs_operate_log_tip (NOLOCK) AS operateLogTips
                                ORDER BY operateLogTips.system_code ,
                                        operateLogTips.source ,
                                        operateLogTips.operate_module ,
                                        operateLogTips.operate_type;").ToList();
                if (query.HasValue())
                {
                    systemCodes = query.Select(p => p.SystemCode).Distinct().ToList();
                    sources = query.Select(p => p.Source).Distinct().ToList();
                    operateModules = query.Select(p => p.OperateModule).Distinct().ToList();
                    operateTypes = query.Select(p => p.OperateType).Distinct().ToList();
                }
            }

            return new Tuple<List<string>, List<string>, List<string>, List<string>>(systemCodes, sources, operateModules, operateTypes);
        }

        /// <summary>
        /// 依据id获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TLogsOperateLog GetById(long id)
        {
            TLogsOperateLog result = null;

            using (var conn= DapperHelper.CreateConnection())
            {
                result = conn.Query<TLogsOperateLog>(@"SELECT  operateLogs.system_code AS SystemCode ,
                                operateLogs.machine_name AS MachineName ,
                                operateLogs.ip_address AS IpAddress ,
                                operateLogs.process_id AS ProcessId ,
                                operateLogs.process_name AS ProcessName ,
                                operateLogs.thread_id AS ThreadId ,
                                operateLogs.thread_name AS ThreadName ,
                                operateLogs.appdomain_name AS AppdomainName ,
                                operateLogs.operated_time AS OperatedTime ,
                                operateLogs.user_id AS UserId ,
                                operateLogs.user_name AS UserName ,
                                operateLogs.operate_module AS OperateModule ,
                                operateLogs.operate_type AS OperateType ,
                                operateLogs.modify_before AS ModifyBefore ,
                                operateLogs.modify_after AS ModifyAfter ,
                                operateLogs.created_time AS CreatedTime ,
                                operateLogs.client_ip AS ClientIp ,
                                *
                        FROM    dbo.t_logs_operate_log (NOLOCK) AS operateLogs
                        WHERE   operateLogs.id = @Id;", new { @Id = id }).FirstOrDefault();
            }

            return result;
        }
    }
}
