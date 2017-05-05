using Log.Entity.Common;
using Log.Entity.Db;
using Log.Entity.RabbitMQ;
using Log.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracy.Frameworks.Common.Result;

namespace Log.IService
{
    /// <summary>
    /// Operate Log操作日志service接口
    /// </summary>
    public interface ILogsOperateLogService
    {
        /// <summary>
        /// 插入操作日志
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ServiceResult<bool> AddOperateLog(AddOperateLogRequest request);

        /// <summary>
        /// 获取所有操作日志(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ServiceResult<PagingResult<GetPagingOperateLogsResponse>> GetPagingOperateLogs(GetPagingOperateLogsRequest request);

        /// <summary>
        /// 刷新智能提示
        /// </summary>
        /// <returns></returns>
        ServiceResult<bool> RefreshOperateLogTip();

        /// <summary>
        /// 获取智能提示数据
        /// </summary>
        /// <returns></returns>
        ServiceResult<Tuple<List<string>, List<string>, List<string>, List<string>>> GetAutoCompleteData();

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ServiceResult<TLogsOperateLog> GetById(long id);

    }
}
