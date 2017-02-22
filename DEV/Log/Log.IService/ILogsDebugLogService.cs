using Log.Entity.Common;
using Log.Entity.ViewModel;
using System.ServiceModel;

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
    }
}
