using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.IService;
using Log.Entity.Common;
using Log.Entity.RabbitMQ;
using Log.Entity.Db;
using Log.IDao;
using EmitMapper;
using Tracy.Frameworks.Common.Result;
using Log.Entity.ViewModel;
using Tracy.Frameworks.Common.Extends;

namespace Log.Service
{
    /// <summary>
    /// Operate Log操作日志service
    /// </summary>
    public class LogsOperateLogService : ILogsOperateLogService
    {
        //注入dao
        private readonly ILogsOperateLogDao _operateLogDao;

        public LogsOperateLogService(ILogsOperateLogDao operateLogDao)
        {
            _operateLogDao = operateLogDao;
        }

        /// <summary>
        /// 插入操作日志
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<bool> AddOperateLog(AddOperateLogRequest request)
        {
            //DTO转数据库实体
            //插入数据库
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            //EmitMapper映射
            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<AddOperateLogRequest, TLogsOperateLog>();
            var item = mapper.Map(request);

            var rs = _operateLogDao.Insert(item);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 获取所有操作日志(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<PagingResult<GetPagingOperateLogsResponse>> GetPagingOperateLogs(GetPagingOperateLogsRequest request)
        {
            var result = new ServiceResult<PagingResult<GetPagingOperateLogsResponse>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new PagingResult<GetPagingOperateLogsResponse>()
            };

            //处理详情页面url
            var logSiteUrl = Log.Common.Helper.ConfigHelper.LogSite;
            var rs = _operateLogDao.GetPagingOperateLogs(request);
            if (rs != null && rs.Entities.HasValue())
            {
                var operateLogs = rs.Entities;
                foreach (var item in operateLogs)
                {
                    item.DetailUrl = string.Format("{0}OperateLog/Detail/{1}", logSiteUrl, item.Id);
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
        public ServiceResult<bool> RefreshOperateLogTip()
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };
            var flag = _operateLogDao.RefreshOperateLogTip();
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
        public ServiceResult<Tuple<List<string>, List<string>, List<string>, List<string>>> GetAutoCompleteData()
        {
            var systemCodes = new List<string>();
            var sources = new List<string>();
            var operateModules = new List<string>();
            var operateTypes = new List<string>();

            var result = new ServiceResult<Tuple<List<string>, List<string>, List<string>, List<string>>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new Tuple<List<string>, List<string>, List<string>, List<string>>(systemCodes, sources, operateModules, operateTypes)
            };

            var rs = _operateLogDao.GetAutoCompleteData();
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

            return result;
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResult<TLogsOperateLog> GetById(long id)
        {
            var result = new ServiceResult<TLogsOperateLog>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new TLogsOperateLog()
            };

            var rs = _operateLogDao.GetById(id);
            if (rs != null)
            {
                //处理modify_before
                if (rs.ModifyBefore != null && rs.ModifyBefore.Length > 0)
                {
                    try
                    {
                        rs.ModifyBeforeDetail = rs.ModifyBefore.LZ4Decompress();
                    }
                    catch
                    {
                    }
                }

                //处理modify_after
                if (rs.ModifyAfter != null && rs.ModifyAfter.Length > 0)
                {
                    try
                    {
                        rs.ModifyAfterDetail = rs.ModifyAfter.LZ4Decompress();
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

    }
}
