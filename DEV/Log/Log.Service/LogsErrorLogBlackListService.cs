using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.IService;
using Log.Entity.Common;
using Tracy.Frameworks.Common.Result;
using Log.Entity.ViewModel;
using Log.IDao;
using Log.Entity.Db;

namespace Log.Service
{
    /// <summary>
    /// error log黑名单service
    /// </summary>
    public class LogsErrorLogBlackListService : ILogsErrorLogBlackListService
    {
        //注入dao
        private ILogsErrorLogBlackListDao _errorLogBlackListDao;

        public LogsErrorLogBlackListService(ILogsErrorLogBlackListDao errorLogBlackListDao)
        {
            _errorLogBlackListDao = errorLogBlackListDao;
        }

        /// <summary>
        /// 获取黑名单列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<PagingResult<GetPagingErrorLogBlackListResponse>> GetPagingBlackList(GetPagingErrorLogBlackListRequest request)
        {
            var result = new ServiceResult<PagingResult<GetPagingErrorLogBlackListResponse>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new PagingResult<GetPagingErrorLogBlackListResponse>()
            };

            var rs = _errorLogBlackListDao.GetPagingBlackList(request);
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

            return result;
        }

        /// <summary>
        /// 添加黑名单
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public ServiceResult<bool> Insert(InsertErrorLogBlackListRequest request, TRightsUser loginInfo)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var currentTime = DateTime.Now;
            var item = new TLogsErrorLogBlackList
            {
                SystemCode = request.SystemCode,
                Source = request.Source,
                MachineName = request.MachineName,
                IpAddress = request.IpAddress,
                ClientIp = request.ClientIp,
                AppdomainName = request.AppdomainName,
                Message = request.Message,
                IsRegex = request.IsRegex,
                CreatedBy = loginInfo.Id,
                CreatedTime = currentTime,
                LastUpdatedBy = loginInfo.Id,
                LastUpdatedTime = currentTime
            };
            var rs = _errorLogBlackListDao.Insert(item);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 删除黑名单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<bool> DeleteBlackList(DeleteErrorLogBlackListRequest request)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var rs = _errorLogBlackListDao.DeleteErrorLogBlackList(request);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }
    }
}
