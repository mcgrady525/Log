using Log.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Tracy.Frameworks.LogClient.Entity;

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
        ServiceResult<bool> AddXmlLog(XmlLog request);

    }
}
