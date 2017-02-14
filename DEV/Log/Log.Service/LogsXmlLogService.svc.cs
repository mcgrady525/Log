using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Log.IService;
using Log.Entity.Common;
using Tracy.Frameworks.LogClient.Entity;
using Log.IDao;
using Log.DaoFactory;
using Log.Entity.Db;

namespace Log.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class LogsXmlLogService : ILogsXmlLogService
    {
        //注入dao
        private static readonly ILogsXmlLogDao xmlLogDao = Factory.GetLogsXmlLogDao();

        /// <summary>
        /// 插入xml log
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<bool> AddXmlLog(XmlLog request)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var item = new TLogsXmlLog
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
                ClassName = request.ClassName,
                MethodName = request.MethodName,
                Rq = request.RQ,
                Rs = request.RS,
                Remark = request.Remark,
                CreatedTime = request.CreatedTime
            };
            var rs = xmlLogDao.Insert(item);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }
    }
}
