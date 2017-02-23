using Log.Entity.Common;
using Log.Entity.ViewModel;
using System.ServiceModel;
using Tracy.Frameworks.Common.Result;
using Log.Entity.Db;

namespace Log.IService
{
    /// <summary>
    /// debug log服务接口
    /// </summary>
    [ServiceContract(ConfigurationName = "LogsDebugLogService.ILogsDebugLogService")]
    public interface ILogsDebugLogService
    {
        /// <summary>
        /// 插入调试日志
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> AddDebugLog(AddDebugLogRequest request);

        /// <summary>
        /// 获取所有调试日志(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<PagingResult<GetPagingDebugLogsResponse>> GetPagingDebugLogs(GetPagingDebugLogsRequest request);

        /// <summary>
        /// 依据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<TLogsDebugLog> GetDebugLogById(int id);

    }
}
