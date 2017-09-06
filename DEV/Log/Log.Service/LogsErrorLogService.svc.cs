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
using Tracy.Frameworks.Common.Result;
using Tracy.Frameworks.Common.Extends;
using Log.Entity.RabbitMQ;
using Tracy.Frameworks.Common.Helpers;
using System.Text.RegularExpressions;
using Log.Common.Helper;
using EmitMapper;

namespace Log.Service
{
    /// <summary>
    /// error log服务
    /// </summary>
    public class LogsErrorLogService : ILogsErrorLogService
    {
        //注入dao
        private readonly ILogsErrorLogDao _errorLogDao;
        private readonly ILogsErrorLogBlackListDao _errorLogBlackListDao;

        public LogsErrorLogService(ILogsErrorLogDao errorLogDao, ILogsErrorLogBlackListDao errorLogBlackListDao)
        {
            _errorLogDao = errorLogDao;
            _errorLogBlackListDao = errorLogBlackListDao;
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

            //如果包含在黑名单中的，直接扔掉不写入db
            var errorLogBlackListCacheKey = "Log.Cache.ErrorLogBlackList";
            var errorLogBlackList = CacheHelper.Get(errorLogBlackListCacheKey) as List<TLogsErrorLogBlackList>;
            if (errorLogBlackList == null)
            {
                errorLogBlackList = _errorLogBlackListDao.GetAll();
                CacheHelper.Set(errorLogBlackListCacheKey, errorLogBlackList);
            }

            if (errorLogBlackList.HasValue())
            {
                var isMatchBlackList = IsMatchErrorLogBlackList(request, errorLogBlackList);
                if (isMatchBlackList)
                {
                    result.ReturnCode = ReturnCodeType.Success;
                    result.Content = true;
                    return result;
                }
            }

            //EmitMapper对象映射
            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<AddErrorLogRequest, TLogsErrorLog>();
            var item = mapper.Map(request);

            var rs = _errorLogDao.Insert(item);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 是否匹配黑名单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private bool IsMatchErrorLogBlackList(AddErrorLogRequest request, List<TLogsErrorLogBlackList> errorLogBlackList)
        {
            //只要任意一个条件匹配即为true
            var message = request.Message.LZ4Decompress();
            var isMatchRegex = false;
            foreach (var item in errorLogBlackList)
            {
                //SystemCode
                if (!item.SystemCode.IsNullOrEmpty() && request.SystemCode.EqualsIgnoreCase(item.SystemCode))
                {
                    return true;
                }

                //Source
                if (!item.Source.IsNullOrEmpty() && request.Source.EqualsIgnoreCase(item.Source))
                {
                    return true;
                }

                //MachineName
                if (!item.MachineName.IsNullOrEmpty() && request.MachineName.EqualsIgnoreCase(item.MachineName))
                {
                    return true;
                }

                //IpAddress
                if (!item.IpAddress.IsNullOrEmpty() && request.IpAddress.Contains(item.IpAddress))
                {
                    return true;
                }

                //ClientIp
                if (!item.ClientIp.IsNullOrEmpty() && request.ClientIp.Contains(item.ClientIp))
                {
                    return true;
                }

                //AppdomainName
                if (!item.AppdomainName.IsNullOrEmpty() && item.AppdomainName.EqualsIgnoreCase(request.AppdomainName))
                {
                    return true;
                }

                //Message
                //正则模式
                if (item.IsRegex.HasValue && item.IsRegex.Value && !item.Message.IsNullOrEmpty())
                {
                    //如果message太长，使用正则会有性能问题，所以最好加上timeout设置
                    try
                    {
                        isMatchRegex = Regex.IsMatch(message, item.Message, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(500));
                    }
                    catch (RegexMatchTimeoutException ex)
                    {
                        //LogHelper.Error(() => string.Format("Timeout after {0} seconds matching {1}", ex.MatchTimeout, ex.Input));
                    }
                    if (isMatchRegex)
                    {
                        return true;
                    }
                }
                else
                {
                    //普通模式
                    if (!item.Message.IsNullOrEmpty() && message.ToLower().Contains(item.Message.ToLower()))
                    {
                        return true;
                    }
                }
            }

            return false;
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
                        try
                        {
                            item.MessageDetail = item.Message.LZ4Decompress();
                        }
                        catch
                        {
                        }
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
                    try
                    {
                        rs.MessageDetail = rs.Message.LZ4Decompress();
                    }
                    catch
                    {
                    }
                }

                //处理detail
                if (rs.Detail != null && rs.Detail.Length > 0)
                {
                    try
                    {
                        rs.LogDetail = rs.Detail.LZ4Decompress();
                    }
                    catch
                    {
                    }
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
