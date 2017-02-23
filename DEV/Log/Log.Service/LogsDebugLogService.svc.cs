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
using Tracy.Frameworks.Common.Result;
using Tracy.Frameworks.Common.Extends;

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
            TinyMapper.Bind<AddDebugLogRequest, TLogsDebugLog>();
            var item = TinyMapper.Map<TLogsDebugLog>(request);

            var rs = debugLogDao.Insert(item);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 获取所有调试日志(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<PagingResult<GetPagingDebugLogsResponse>> GetPagingDebugLogs(GetPagingDebugLogsRequest request)
        {
            var result = new ServiceResult<PagingResult<GetPagingDebugLogsResponse>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new PagingResult<GetPagingDebugLogsResponse>()
            };

            //处理详情页面url
            var logSiteUrl = Log.Common.Helper.ConfigHelper.LogSite;
            var rs = debugLogDao.GetPagingDebugLogs(request);
            if (rs != null && rs.Entities.HasValue())
            {
                var debugLogs = rs.Entities;
                foreach (var item in debugLogs)
                {
                    item.DetailUrl = string.Format("{0}DebugLog/Detail/{1}", logSiteUrl, item.Id);
                }
            }

            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

            return result;
        }

        /// <summary>
        /// 依据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResult<TLogsDebugLog> GetDebugLogById(int id)
        {
            var result = new ServiceResult<TLogsDebugLog>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new TLogsDebugLog()
            };

            var rs = debugLogDao.GetById(id);
            if (rs != null)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = rs;
            }

            return result;
        }


    }
}
