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
using Log.Entity.Db;
using Log.Entity.ViewModel;
using Nelibur.ObjectMapper;
using Nelibur.ObjectMapper.Bindings;
using Tracy.Frameworks.Common.Result;
using Tracy.Frameworks.Common.Extends;

namespace Log.Service
{
    /// <summary>
    /// error log服务
    /// </summary>
    public class LogsErrorLogService : ILogsErrorLogService
    {
        //注入dao
        private readonly ILogsErrorLogDao _errorLogDao;

        public LogsErrorLogService(ILogsErrorLogDao errorLogDao)
        {
            _errorLogDao = errorLogDao;
        }

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

            var rs = _errorLogDao.Insert(item);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 刷新错误日志的智能提示
        /// </summary>
        /// <returns></returns>
        public ServiceResult<bool> RefreshErrorLogTip()
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };
            var flag = _errorLogDao.RefreshErrorLogTip();
            if (flag)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 获取所有错误日志(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<PagingResult<GetPagingErrorLogsResponse>> GetPagingErrorLogs(GetPagingErrorLogsRequest request)
        {
            var result = new ServiceResult<PagingResult<GetPagingErrorLogsResponse>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new PagingResult<GetPagingErrorLogsResponse>()
            };

            //处理详情页面url
            var logSiteUrl = Log.Common.Helper.ConfigHelper.LogSite;
            var rs = _errorLogDao.GetPagingErrorLogs(request);
            if (rs != null && rs.Entities.HasValue())
            {
                foreach (var item in rs.Entities)
                {
                    item.DetailUrl = string.Format("{0}ErrorLog/Detail/{1}", logSiteUrl, item.Id);

                    //处理message
                    if (item.Message != null && item.Message.Length > 0)
                    {
                        item.MessageDetail = item.Message.LZ4Decompress();
                    }
                }
            }

            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

            return result;
        }

        /// <summary>
        /// 依据id查询错误日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResult<TLogsErrorLog> GetErrorLogById(int id)
        {
            var result = new ServiceResult<TLogsErrorLog>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new TLogsErrorLog()
            };

            var rs = _errorLogDao.GetById(id);
            if (rs != null)
            {
                //处理message
                if (rs.Message != null && rs.Message.Length > 0)
                {
                    rs.MessageDetail = rs.Message.LZ4Decompress();
                }

                //处理detail
                if (rs.Detail != null && rs.Detail.Length > 0)
                {
                    rs.LogDetail = rs.Detail.LZ4Decompress();
                }

                result.ReturnCode = ReturnCodeType.Success;
                result.Content = rs;
            }

            return result;
        }

        /// <summary>
        /// 获取智能提示数据
        /// </summary>
        /// <returns></returns>
        public ServiceResult<Tuple<List<string>, List<string>>> GetAutoCompleteData()
        {
            var systemCodes = new List<string>();
            var sources = new List<string>();
            var result = new ServiceResult<Tuple<List<string>, List<string>>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new Tuple<List<string>, List<string>>(systemCodes, sources)
            };

            var rs = _errorLogDao.GetAutoCompleteData();
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

            return result;
        }
    }
}
