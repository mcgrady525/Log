using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.IService;
using System.ServiceModel.Activation;
using System.ServiceModel;
using Log.Entity.Common;
using Log.IDao;
using Log.DaoFactory;
using Log.Entity.Db;
using Log.Entity.ViewModel;
using Nelibur.ObjectMapper;
using Nelibur.ObjectMapper.Bindings;

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
        public ServiceResult<bool> AddErrorLog(AddErrorLogRequest request)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            //TinyMapper对象映射
            TinyMapper.Bind<AddErrorLogRequest, TLogsErrorLog>();
            var item = TinyMapper.Map<TLogsErrorLog>(request);

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
