using Log.Entity.Common;
using Log.Entity.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Tracy.Frameworks.LogClient.Entity;

namespace Log.IService
{
    [ServiceContract(ConfigurationName = "LogsDebugLogService.ILogsDebugLogService")]
    public interface ILogsDebugLogService
    {
        /// <summary>
        /// 插入调试日志
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> AddDebugLog(DebugLog request);
    }
}
