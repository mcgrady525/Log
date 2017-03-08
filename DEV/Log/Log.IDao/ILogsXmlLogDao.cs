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
    /// xml log
    /// </summary>
    public interface ILogsXmlLogDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        bool Insert(TLogsXmlLog item);

        /// <summary>
        /// 刷新xml日志的智能提示
        /// </summary>
        /// <returns></returns>
        bool RefreshXmlLogTip();

        /// <summary>
        /// 获取所有日志(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PagingResult<GetPagingXmlLogsResponse> GetPagingXmlLogs(GetPagingXmlLogsRequest request);

        /// <summary>
        /// 获取智能提示数据源
        /// </summary>
        /// <returns></returns>
        Tuple<List<string>, List<string>, List<string>, List<string>> GetAutoCompleteData();

        /// <summary>
        /// 依id查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        TLogsXmlLog GetById(long id);
    }
}
