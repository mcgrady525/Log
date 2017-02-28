using Log.Entity.Common;
using Log.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Log.IService
{
    /// <summary>
    /// xml log服务接口
    /// </summary>
    [ServiceContract(ConfigurationName = "LogsXmlLogService.ILogsXmlLogService")]
    public interface ILogsXmlLogService
    {
        /// <summary>
        /// 插入xml log
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> AddXmlLog(AddXmlLogRequest request);

        /// <summary>
        /// 刷新xml日志的智能提示
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> RefreshXmlLogTip();

    }
}
