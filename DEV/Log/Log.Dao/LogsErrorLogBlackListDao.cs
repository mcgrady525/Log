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

namespace Log.Dao
{
    /// <summary>
    /// error log黑名单dao
    /// </summary>
    public class LogsErrorLogBlackListDao : ILogsErrorLogBlackListDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        public bool Insert(TLogsErrorLogBlackList item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"INSERT INTO dbo.t_logs_error_log_black_list VALUES (@SystemCode,@Source ,@MachineName ,@IpAddress ,@ClientIp ,@AppdomainName ,@Message ,@IsRegex ,@CreatedBy ,@CreatedTime ,@LastUpdatedBy,@LastUpdatedTime);", item);
                if (effectRows > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="item">待更新的记录</param>
        /// <returns></returns>
        public bool Update(TLogsErrorLogBlackList item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">待删除记录的id</param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"DELETE FROM dbo.t_logs_error_log_black_list WHERE id= @Id;", new { @Id = id });
                if (effectRows > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">id列表</param>
        /// <returns></returns>
        public bool BatchDelete(List<long> ids)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"DELETE FROM dbo.t_logs_error_log_black_list WHERE id IN @Ids;", new { @Ids = ids });
                if (effectRows > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 依id查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public TLogsErrorLogBlackList GetById(long id)
        {
            TLogsErrorLogBlackList result = null;

            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TLogsErrorLogBlackList>(@"SELECT  errorLogBlackList.system_code AS SystemCode ,
                                    errorLogBlackList.machine_name AS MachineName ,
                                    errorLogBlackList.ip_address AS IpAddress ,
                                    errorLogBlackList.client_ip AS ClientIp ,
                                    errorLogBlackList.appdomain_name AS AppdomainName ,
                                    errorLogBlackList.is_regex AS IsRegex ,
                                    errorLogBlackList.created_by AS CreatedBy ,
                                    errorLogBlackList.created_time AS CreatedTime ,
                                    errorLogBlackList.last_updated_by AS LastUpdatedBy ,
                                    errorLogBlackList.last_updated_time AS LastUpdatedTime ,
                                    *
                            FROM    dbo.t_logs_error_log_black_list (NOLOCK) AS errorLogBlackList
                            WHERE   errorLogBlackList.id = @Id;", new { @Id = id }).FirstOrDefault();
            }

            return result;
        }

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<TLogsErrorLogBlackList> GetAll()
        {
            List<TLogsErrorLogBlackList> result = null;

            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TLogsErrorLogBlackList>(@"SELECT  errorLogBlackList.system_code AS SystemCode ,
                                    errorLogBlackList.machine_name AS MachineName ,
                                    errorLogBlackList.ip_address AS IpAddress ,
                                    errorLogBlackList.client_ip AS ClientIp ,
                                    errorLogBlackList.appdomain_name AS AppdomainName ,
                                    errorLogBlackList.is_regex AS IsRegex ,
                                    errorLogBlackList.created_by AS CreatedBy ,
                                    errorLogBlackList.created_time AS CreatedTime ,
                                    errorLogBlackList.last_updated_by AS LastUpdatedBy ,
                                    errorLogBlackList.last_updated_time AS LastUpdatedTime ,
                                    *
                            FROM    dbo.t_logs_error_log_black_list (NOLOCK) AS errorLogBlackList
                            ORDER BY errorLogBlackList.id DESC;").ToList();
            }

            return result;
        }

        /// <summary>
        /// 获取黑名单列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagingResult<GetPagingErrorLogBlackListResponse> GetPagingBlackList(GetPagingErrorLogBlackListRequest request)
        {
            PagingResult<GetPagingErrorLogBlackListResponse> result = null;
            var totalCount = 0;
            var startIndex = (request.PageIndex - 1) * request.PageSize + 1;
            var endIndex = request.PageIndex * request.PageSize;

            using (var conn = DapperHelper.CreateConnection())
            {
                var multi = conn.QueryMultiple(@"--获取所有(分页)
                                SELECT  rs.*
                                FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY errorLogBlackList.id DESC ) AS RowNum ,
                                                    errorLogBlackList.system_code AS SystemCode ,
                                                    errorLogBlackList.machine_name AS MachineName ,
                                                    errorLogBlackList.ip_address AS IpAddress ,
                                                    errorLogBlackList.client_ip AS ClientIp ,
                                                    errorLogBlackList.appdomain_name AS AppdomainName ,
                                                    errorLogBlackList.is_regex AS IsRegex ,
                                                    errorLogBlackList.created_time AS CreatedTime ,
                                                    errorLogBlackList.last_updated_time AS LastUpdatedTime ,
                                                    *
                                          FROM      dbo.t_logs_error_log_black_list (NOLOCK) AS errorLogBlackList
                                        ) AS rs
                                WHERE   rs.RowNum BETWEEN @Start AND @End;

                                --获取所有total
                                SELECT  COUNT(errorLogBlackList.id)
                                FROM    dbo.t_logs_error_log_black_list (NOLOCK) AS errorLogBlackList;", new { @Start = startIndex, @End = endIndex });
                var query1 = multi.Read<GetPagingErrorLogBlackListResponse>();
                var query2 = multi.Read<int>();
                totalCount = query2.First();

                result = new PagingResult<GetPagingErrorLogBlackListResponse>(totalCount, request.PageIndex, request.PageSize, query1);
            }

            return result;
        }

        /// <summary>
        /// 删除黑名单(支持批量)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool DeleteErrorLogBlackList(DeleteErrorLogBlackListRequest request)
        {
            var ids = request.Ids.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.ToLong()).ToList();
            return BatchDelete(ids);
        }
    }
}
