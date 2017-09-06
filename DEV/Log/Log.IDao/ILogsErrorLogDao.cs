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
    /// error log
    /// </summary>
    public interface ILogsErrorLogDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        bool Insert(TLogsErrorLog item);

        /// <summary>
        /// 刷新错误日志的智能提示
        /// </summary>
        /// <returns></returns>
        bool RefreshErrorLogTip();

        /// <summary>
        /// 获取所有错误日志(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PagingResult<GetPagingErrorLogsResponse> GetPagingErrorLogs(GetPagingErrorLogsRequest request);

        /// <summary>
        /// 依id查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        TLogsErrorLog GetById(int id);

        /// <summary>
        /// 获取智能提示数据
        /// </summary>
        /// <returns></returns>
        Tuple<List<string>, List<string>> GetAutoCompleteData();

    }
}
