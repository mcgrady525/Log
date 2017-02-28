using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Log.IService;
using Log.Entity.Common;
using Log.IDao;
using Log.DaoFactory;
using Log.Entity.Db;
using Log.Entity.ViewModel;
using Nelibur.ObjectMapper;
using Nelibur.ObjectMapper.Bindings;

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
        public ServiceResult<bool> AddXmlLog(AddXmlLogRequest request)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            //TinyMapper对象映射
            TinyMapper.Bind<AddXmlLogRequest, TLogsXmlLog>();
            var item = TinyMapper.Map<TLogsXmlLog>(request);

            var rs = xmlLogDao.Insert(item);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 刷新xml日志的智能提示
        /// </summary>
        /// <returns></returns>
        public ServiceResult<bool> RefreshXmlLogTip()
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };
            var flag = xmlLogDao.RefreshXmlLogTip();
            if (flag)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }
    }
}
