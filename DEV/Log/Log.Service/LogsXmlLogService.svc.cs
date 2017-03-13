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
using Log.Entity.Db;
using Log.Entity.ViewModel;
using Nelibur.ObjectMapper;
using Nelibur.ObjectMapper.Bindings;
using Tracy.Frameworks.Common.Result;
using Tracy.Frameworks.Common.Extends;

namespace Log.Service
{
    /// <summary>
    /// xml日志service
    /// </summary>
    public class LogsXmlLogService : ILogsXmlLogService
    {
        //注入dao
        private readonly ILogsXmlLogDao _xmlLogDao;

        public LogsXmlLogService(ILogsXmlLogDao xmlLogDao)
        {
            _xmlLogDao = xmlLogDao;
        }

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

            var rs = _xmlLogDao.Insert(item);
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
            var flag = _xmlLogDao.RefreshXmlLogTip();
            if (flag)
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
        public ServiceResult<PagingResult<GetPagingXmlLogsResponse>> GetPagingXmlLogs(GetPagingXmlLogsRequest request)
        {
            var result = new ServiceResult<PagingResult<GetPagingXmlLogsResponse>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new PagingResult<GetPagingXmlLogsResponse>()
            };

            //处理详情页面url
            var logSiteUrl = Log.Common.Helper.ConfigHelper.LogSite;
            var rs = _xmlLogDao.GetPagingXmlLogs(request);
            if (rs != null && rs.Entities.HasValue())
            {
                foreach (var item in rs.Entities)
                {
                    item.DetailUrl = string.Format("{0}XmlLog/Detail/{1}", logSiteUrl, item.Id);
                }
            }
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

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

            var rs = _xmlLogDao.GetAutoCompleteData();
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

            return result;
        }

        /// <summary>
        /// 依据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResult<TLogsXmlLog> GetXmlLogById(long id)
        {
            var result = new ServiceResult<TLogsXmlLog>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new TLogsXmlLog()
            };

            var rs = _xmlLogDao.GetById(id);
            if (rs != null)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = rs;
            }

            return result;
        }
    }
}
