using Log.Entity.Db;
using Log.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracy.Frameworks.Common.Result;

namespace Log.IDao
{
    /// <summary>
    /// 性能日志(perf log)Dao接口
    /// </summary>
    public interface ILogsPerformanceLogDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        bool Insert(TLogsPerformanceLog item);

        /// <summary>
        /// 获取所有日志(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PagingResult<GetPagingPerformanceLogsResponse> GetPagingPerformanceLogs(GetPagingPerformanceLogsRequest request);

        /// <summary>
        /// 刷新智能提示
        /// </summary>
        /// <returns></returns>
        bool RefreshPerfLogTip();

        /// <summary>
        /// 获取智能提示数据源
        /// </summary>
        /// <returns></returns>
        Tuple<List<string>, List<string>, List<string>, List<string>> GetAutoCompleteData();

        /// <summary>
        /// 依据id获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TLogsPerformanceLog GetById(long id);
    }
}
