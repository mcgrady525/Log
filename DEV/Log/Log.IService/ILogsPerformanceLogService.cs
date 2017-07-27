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
    /// 性能日志service接口
    /// </summary>
    [ServiceContract(ConfigurationName = "LogsPerformanceLogService.ILogsPerformanceLogService")]
    public interface ILogsPerformanceLogService
    {
        /// <summary>
        /// 插入perf log
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> AddPerfLog(AddPerformanceLogRequest request);

        /// <summary>
        /// 获取所有日志(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<PagingResult<GetPagingPerformanceLogsResponse>> GetPagingPerformanceLogs(GetPagingPerformanceLogsRequest request);

        /// <summary>
        /// 刷新智能提示
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> RefreshPerfLogTip();

        /// <summary>
        /// 获取智能提示数据源
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<Tuple<List<string>, List<string>, List<string>, List<string>, List<string>>> GetAutoCompleteData();

        /// <summary>
        /// 依据id获取perf log
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<TLogsPerformanceLog> GetPerfLogById(long id);
    }
}
