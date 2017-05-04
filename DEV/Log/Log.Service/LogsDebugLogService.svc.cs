﻿using System;
using System.Collections.Generic;
using System.Linq;
using Log.IService;
using Log.IDao;
using Log.Entity.Common;
using Log.Entity.Db;
using Log.Entity.ViewModel;
using Tracy.Frameworks.Common.Result;
using Tracy.Frameworks.Common.Extends;
using Log.Entity.RabbitMQ;
using Tracy.Frameworks.Common.Helpers;
using System.Text.RegularExpressions;
using EmitMapper;

namespace Log.Service
{
    /// <summary>
    /// debug log服务
    /// </summary>
    public class LogsDebugLogService : ILogsDebugLogService
    {
        //注入dao
        private readonly ILogsDebugLogDao _debugLogDao;
        private readonly ILogsDebugLogBlackListDao _debugLogBlackListDao;

        public LogsDebugLogService(ILogsDebugLogDao debugDao, ILogsDebugLogBlackListDao debugLogBlackListDao)
        {
            _debugLogDao = debugDao;
            _debugLogBlackListDao = debugLogBlackListDao;
        }

        /// <summary>
        /// 新增调试日志
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<bool> AddDebugLog(AddDebugLogRequest request)
        {
            //增加黑名单功能
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            //如果包含在黑名单中的，直接扔掉不写入db
            var debugLogBlackListCacheKey = "Log.Cache.DebugLogBlackList";
            var debugLogBlackList = CacheHelper.Get(debugLogBlackListCacheKey) as List<TLogsDebugLogBlackList>;
            if (debugLogBlackList == null)
            {
                debugLogBlackList = _debugLogBlackListDao.GetAll();
                CacheHelper.Set(debugLogBlackListCacheKey, debugLogBlackList);
            }
            if (debugLogBlackList.HasValue())
            {
                var isMatchBlackList = IsMatchDebugLogBlackList(request, debugLogBlackList);
                if (isMatchBlackList)
                {
                    result.ReturnCode = ReturnCodeType.Success;
                    result.Content = true;
                    return result;
                }
            }

            //EmitMapper对象映射
            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<AddDebugLogRequest, TLogsDebugLog>();
            var item = mapper.Map(request);

            var rs = _debugLogDao.Insert(item);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 插入调试日志(批量)
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public ServiceResult<bool> AddDebugLogs(List<AddDebugLogRequest> list)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            if (!list.HasValue())
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
                return result;
            }

            //黑名单过滤
            var matchedDebugLogs = new List<AddDebugLogRequest>();
            var debugLogBlackListCacheKey = "Log.Cache.DebugLogBlackList";
            var debugLogBlackList = CacheHelper.Get(debugLogBlackListCacheKey) as List<TLogsDebugLogBlackList>;
            if (debugLogBlackList == null)
            {
                debugLogBlackList = _debugLogBlackListDao.GetAll();
                CacheHelper.Set(debugLogBlackListCacheKey, debugLogBlackList);
            }

            if (debugLogBlackList.HasValue())
            {
                foreach (var debugLog in list)
                {
                    var isMatchBlackList = IsMatchDebugLogBlackList(debugLog, debugLogBlackList);
                    if (isMatchBlackList)
                    {
                        matchedDebugLogs.Add(debugLog);
                    }
                }
            }

            list = list.Except(matchedDebugLogs).ToList();
            if (!list.HasValue())
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
                return result;
            }

            //EmitMapper对象映射
            var insertedDebugLogs = new List<TLogsDebugLog>();
            TLogsDebugLog insertedDebugLog = null;
            foreach (var debugLog in list)
            {
                var mapper = ObjectMapperManager.DefaultInstance.GetMapper<AddDebugLogRequest, TLogsDebugLog>();
                insertedDebugLog = mapper.Map(debugLog);
                insertedDebugLogs.Add(insertedDebugLog);
            }

            var rs = _debugLogDao.BatchInsert(insertedDebugLogs);
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
        private bool IsMatchDebugLogBlackList(AddDebugLogRequest request, List<TLogsDebugLogBlackList> debugLogBlackList)
        {
            //只要任意一个条件匹配即为true
            var message = request.Message.LZ4Decompress();
            var isMatchRegex = false;
            foreach (var item in debugLogBlackList)
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
            var rs = _debugLogDao.GetPagingDebugLogs(request);
            if (rs != null && rs.Entities.HasValue())
            {
                var debugLogs = rs.Entities;
                foreach (var item in debugLogs)
                {
                    item.DetailUrl = string.Format("{0}DebugLog/Detail/{1}", logSiteUrl, item.Id);

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

            var rs = _debugLogDao.GetById(id);
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
        /// 刷新调试日志的智能提示
        /// </summary>
        /// <returns></returns>
        public ServiceResult<bool> RefreshDebugLogTip()
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };
            var flag = _debugLogDao.RefreshDebugLogTip();
            if (flag)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
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

            var rs = _debugLogDao.GetAutoCompleteData();
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

            return result;
        }


    }
}
