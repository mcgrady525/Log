using Log.DaoFactory;
using Log.Entity.Common;
using Log.Entity.Db;
using Log.Entity.ViewModel;
using Log.IDao;
using Log.IService;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Tracy.Frameworks.Common.Result;
using Tracy.Frameworks.Common.Extends;

namespace Log.Service
{
    /// <summary>
    /// 性能日志service
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class LogsPerformanceLogService : ILogsPerformanceLogService
    {
        //注入dao
        private static readonly ILogsPerformanceLogDao perfLogDao = Factory.GetLogsPerformanceLogDao();


        /// <summary>
        /// 插入perf log
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<bool> AddPerfLog(AddPerformanceLogRequest request)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            //TinyMapper对象映射
            TinyMapper.Bind<AddPerformanceLogRequest, TLogsPerformanceLog>();
            var item = TinyMapper.Map<TLogsPerformanceLog>(request);

            var rs = perfLogDao.Insert(item);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 获取所有日志(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<PagingResult<GetPagingPerformanceLogsResponse>> GetPagingPerformanceLogs(GetPagingPerformanceLogsRequest request)
        {
            var result = new ServiceResult<PagingResult<GetPagingPerformanceLogsResponse>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new PagingResult<GetPagingPerformanceLogsResponse>()
            };

            //处理详情页面url
            var logSiteUrl = Log.Common.Helper.ConfigHelper.LogSite;
            var rs = perfLogDao.GetPagingPerformanceLogs(request);
            if (rs != null && rs.Entities.HasValue())
            {
                foreach (var item in rs.Entities)
                {
                    item.DetailUrl = string.Format("{0}PerformanceLog/Detail/{1}", logSiteUrl, item.Id);
                }
            }
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

            return result;
        }

        /// <summary>
        /// 刷新智能提示
        /// </summary>
        /// <returns></returns>
        public ServiceResult<bool> RefreshPerfLogTip()
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var rs = perfLogDao.RefreshPerfLogTip();
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 获取智能提示数据源
        /// </summary>
        /// <returns></returns>
        public ServiceResult<Tuple<List<string>, List<string>, List<string>, List<string>>> GetAutoCompleteData()
        {
            var systemCodes = new List<string>();
            var sources = new List<string>();
            var classNames = new List<string>();
            var methodNames = new List<string>();
            var result = new ServiceResult<Tuple<List<string>, List<string>, List<string>, List<string>>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new Tuple<List<string>, List<string>, List<string>, List<string>>(systemCodes, sources, classNames, methodNames)
            };

            var rs = perfLogDao.GetAutoCompleteData();
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

            return result;
        }

        /// <summary>
        /// 依据id获取perf log
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResult<TLogsPerformanceLog> GetPerfLogById(long id)
        {
            var result = new ServiceResult<TLogsPerformanceLog>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new TLogsPerformanceLog()
            };

            var rs = perfLogDao.GetById(id);
            if (rs != null)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = rs;
            }

            return result;
        }
    }
}
