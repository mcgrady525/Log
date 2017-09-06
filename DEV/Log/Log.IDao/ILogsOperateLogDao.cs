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
    /// OperateLog操作日志dao接口
    /// </summary>
    public interface ILogsOperateLogDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        bool Insert(TLogsOperateLog item);

        /// <summary>
        /// 获取所有操作日志(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PagingResult<GetPagingOperateLogsResponse> GetPagingOperateLogs(GetPagingOperateLogsRequest request);

        /// <summary>
        /// 刷新智能提示
        /// </summary>
        /// <returns></returns>
        bool RefreshOperateLogTip();

        /// <summary>
        /// 获取智能提示数据
        /// </summary>
        /// <returns></returns>
        Tuple<List<string>, List<string>, List<string>, List<string>> GetAutoCompleteData();

        /// <summary>
        /// 依据id获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TLogsOperateLog GetById(long id);
    }
}
