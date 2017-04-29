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
    /// 调试日志
    /// </summary>
    public interface ILogsDebugLogDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        bool Insert(TLogsDebugLog item);

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        bool BatchInsert(List<TLogsDebugLog> list);

        /// <summary>
        /// 获取所有调试日志(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PagingResult<GetPagingDebugLogsResponse> GetPagingDebugLogs(GetPagingDebugLogsRequest request);

        /// <summary>
        /// 依id查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        TLogsDebugLog GetById(int id);

        /// <summary>
        /// 刷新调试日志的智能提示
        /// </summary>
        /// <returns></returns>
        bool RefreshDebugLogTip();

        /// <summary>
        /// 获取智能提示数据
        /// </summary>
        /// <returns></returns>
        Tuple<List<string>, List<string>> GetAutoCompleteData();

    }
}
