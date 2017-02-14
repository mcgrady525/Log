using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.IService;
using System.ServiceModel.Activation;
using System.ServiceModel;
using Log.Entity.Common;
using Tracy.Frameworks.LogClient.Entity;
using Log.IDao;
using Log.DaoFactory;
using Log.Entity.Db;

namespace Log.Service
{
    /// <summary>
    /// error log服务
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class LogsErrorLogService : ILogsErrorLogService
    {
        //注入dao
        private static readonly ILogsErrorLogDao errorLogDao = Factory.GetLogsErrorLogDao();

        /// <summary>
        /// 插入error log
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<bool> AddErrorLog(ErrorLog request)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var item = new TLogsErrorLog
            {
                SystemCode = request.SystemCode,
                Source = request.Source,
                MachineName = request.MachineName,
                IpAddress = request.IPAddress,
                ProcessId = request.ProcessID,
                ProcessName = request.ProcessName,
                ThreadId = request.ThreadID,
                ThreadName = request.ThreadName,
                AppdomainName = request.AppDomainName,
                Message = request.Message,
                Detail = request.Detail,
                CreatedTime = request.CreatedTime
            };
            var rs = errorLogDao.Insert(item);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }
    }
}
