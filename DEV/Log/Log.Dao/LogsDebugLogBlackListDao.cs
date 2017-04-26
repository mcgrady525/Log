using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.IDao;
using Log.Entity.Db;
using Log.Common.Helper;
using Dapper;
using Log.Entity.ViewModel;
using Tracy.Frameworks.Common.Extends;
using Tracy.Frameworks.Common.Result;

namespace Log.Dao
{
    /// <summary>
    /// debug log黑名单dao
    /// </summary>
    public class LogsDebugLogBlackListDao : ILogsDebugLogBlackListDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        public bool Insert(TLogsDebugLogBlackList item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"INSERT INTO dbo.t_logs_debug_log_black_list VALUES (@SystemCode ,@Source ,@MachineName ,@IpAddress ,@ClientIp ,@AppdomainName ,@Message ,@IsRegex ,@CreatedBy ,@CreatedTime ,@LastUpdatedBy ,@LastUpdatedTime);", item);
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
        public bool Update(TLogsDebugLogBlackList item)
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
                var effectRows = conn.Execute(@"DELETE FROM dbo.t_logs_debug_log_black_list WHERE id= @Id;", new { @Id = id });
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
                var effectRows = conn.Execute(@"DELETE FROM dbo.t_logs_debug_log_black_list WHERE id IN @Ids;", new { @Ids = ids });
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
        public TLogsDebugLogBlackList GetById(long id)
        {
            TLogsDebugLogBlackList result = null;

            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TLogsDebugLogBlackList>(@"SELECT  debugLogBlackList.system_code AS SystemCode ,
                                    debugLogBlackList.machine_name AS MachineName ,
                                    debugLogBlackList.ip_address AS IpAddress ,
                                    debugLogBlackList.client_ip AS ClientIp ,
                                    debugLogBlackList.appdomain_name AS AppdomainName ,
                                    debugLogBlackList.is_regex AS IsRegex ,
                                    debugLogBlackList.created_by AS CreatedBy ,
                                    debugLogBlackList.created_time AS CreatedTime ,
                                    debugLogBlackList.last_updated_by AS LastUpdatedBy ,
                                    debugLogBlackList.last_updated_time AS LastUpdatedTime ,
                                    *
                            FROM    dbo.t_logs_debug_log_black_list (NOLOCK) AS debugLogBlackList
                            WHERE   debugLogBlackList.id = @Id;", new { @Id = id }).FirstOrDefault();

            }

            return result;
        }

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<TLogsDebugLogBlackList> GetAll()
        {
            List<TLogsDebugLogBlackList> result = null;

            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TLogsDebugLogBlackList>(@"SELECT  debugLogBlackList.system_code AS SystemCode ,
                                    debugLogBlackList.machine_name AS MachineName ,
                                    debugLogBlackList.ip_address AS IpAddress ,
                                    debugLogBlackList.client_ip AS ClientIp ,
                                    debugLogBlackList.appdomain_name AS AppdomainName ,
                                    debugLogBlackList.is_regex AS IsRegex ,
                                    debugLogBlackList.created_by AS CreatedBy ,
                                    debugLogBlackList.created_time AS CreatedTime ,
                                    debugLogBlackList.last_updated_by AS LastUpdatedBy ,
                                    debugLogBlackList.last_updated_time AS LastUpdatedTime ,
                                    *
                            FROM    dbo.t_logs_debug_log_black_list (NOLOCK) AS debugLogBlackList
                            ORDER BY debugLogBlackList.id DESC;").ToList();

            }

            return result;
        }

        /// <summary>
        /// 删除debug log黑名单(支持批量操作)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool DeleteDebugLogBlackList(DeleteDebugLogBlackListRequest request)
        {
            var ids = request.Ids.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.ToLong()).ToList();
            return BatchDelete(ids);
        }

        /// <summary>
        /// 获取黑名单列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagingResult<GetPagingDebugLogBlackListResponse> GetPagingBlackList(GetPagingDebugLogBlackListRequest request)
        {
            PagingResult<GetPagingDebugLogBlackListResponse> result = null;
            var totalCount = 0;
            var startIndex = (request.PageIndex - 1) * request.PageSize + 1;
            var endIndex = request.PageIndex * request.PageSize;

            using (var conn= DapperHelper.CreateConnection())
            {
                var multi = conn.QueryMultiple(@"--获取所有(分页)
                                SELECT  rs.*
                                FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY debugLogBlackList.id DESC ) AS RowNum ,
                                                    debugLogBlackList.system_code AS SystemCode ,
                                                    debugLogBlackList.machine_name AS MachineName ,
                                                    debugLogBlackList.ip_address AS IpAddress ,
                                                    debugLogBlackList.client_ip AS ClientIp ,
                                                    debugLogBlackList.appdomain_name AS AppdomainName ,
                                                    debugLogBlackList.is_regex AS IsRegex ,
                                                    debugLogBlackList.created_time AS CreatedTime ,
                                                    debugLogBlackList.last_updated_time AS LastUpdatedTime ,
                                                    *
                                          FROM      dbo.t_logs_debug_log_black_list (NOLOCK) AS debugLogBlackList
                                        ) AS rs
                                WHERE   rs.RowNum BETWEEN @Start AND @End;

                                --获取所有total
                                SELECT  COUNT(debugLogBlackList.id)
                                FROM    dbo.t_logs_debug_log_black_list (NOLOCK) AS debugLogBlackList;", new { @Start = startIndex, @End = endIndex });
                var query1 = multi.Read<GetPagingDebugLogBlackListResponse>();
                var query2 = multi.Read<int>();
                totalCount = query2.First();

                result = new PagingResult<GetPagingDebugLogBlackListResponse>(totalCount, request.PageIndex, request.PageSize, query1);
            }

            return result;
        }
    }
}
