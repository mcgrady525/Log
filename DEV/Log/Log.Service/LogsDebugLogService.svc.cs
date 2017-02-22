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
using Log.Entity.ViewModel;
using Nelibur.ObjectMapper;
using Nelibur.ObjectMapper.Bindings;

namespace Log.Service
{
    /// <summary>
    /// debug log服务
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class LogsDebugLogService : ILogsDebugLogService
    {
        //注入dao
        private static readonly ILogsDebugLogDao debugLogDao = Factory.GetLogsDebugLogDao();

        /// <summary>
        /// 新增调试日志
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<bool> AddDebugLog(AddDebugLogRequest request)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            //TinyMapper对象映射
            TinyMapper.Bind<AddDebugLogRequest,TLogsDebugLog>();
            var item = TinyMapper.Map<TLogsDebugLog>(request);

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
