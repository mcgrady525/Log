using Log.Entity.Common;
using Log.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Tracy.Frameworks.Common.Result;
using Log.Entity.Db;
using Log.Entity.RabbitMQ;

namespace Log.IService
{
    /// <summary>
    /// error log服务接口
    /// </summary>
    [ServiceContract(ConfigurationName = "LogsErrorLogService.ILogsErrorLogService")]
    public interface ILogsErrorLogService
    {
        /// <summary>
        /// 插入error log
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> AddErrorLog(AddErrorLogRequest request);

        /// <summary>
        /// 刷新错误日志的智能提示
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> RefreshErrorLogTip();

        /// <summary>
        /// 获取所有错误日志(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<PagingResult<GetPagingErrorLogsResponse>> GetPagingErrorLogs(GetPagingErrorLogsRequest request);

        /// <summary>
        /// 依据id查询错误日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<TLogsErrorLog> GetErrorLogById(int id);

        /// <summary>
        /// 获取智能提示数据
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<Tuple<List<string>, List<string>>> GetAutoCompleteData();
    }
}
