using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.IService;
using System.ServiceModel.Activation;
using System.ServiceModel;
using Log.IDao;
using Log.DaoFactory;
using Log.Entity.Common;
using Log.Entity.Db;
using Tracy.Frameworks.LogClient.Entity;

namespace Log.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class LogsDebugLogService : ILogsDebugLogService
    {
        private static readonly ILogsDebugLogDao debugLogDao = Factory.GetLogsDebugLogDao();

        /// <summary>
        /// 插入调试日志
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<bool> AddDebugLog(DebugLog request)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var item = new TLogsDebugLog
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
            var rs = debugLogDao.Insert(item);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }


    }
}
