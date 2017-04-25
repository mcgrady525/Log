using Log.Entity.Common;
using Log.Entity.Db;
using Log.Entity.ViewModel;
using Log.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracy.Frameworks.Common.Result;
using Log.IDao;

namespace Log.Service
{
    /// <summary>
    /// debug log黑名单service
    /// </summary>
    public class LogsDebugLogBlackListService : ILogsDebugLogBlackListService
    {
        //注入dao
        private ILogsDebugLogBlackListDao _debugLogBlackListDao;

        public LogsDebugLogBlackListService(ILogsDebugLogBlackListDao debugLogBlackListDao)
        {
            _debugLogBlackListDao = debugLogBlackListDao;
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public ServiceResult<bool> Insert(InsertDebugLogBlackListRequest request, TRightsUser loginInfo)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var currentTime = DateTime.Now;
            var item = new TLogsDebugLogBlackList
            {
                Content = request.KeyWord,
                CreatedBy = loginInfo.Id,
                CreatedTime = currentTime,
                LastUpdatedBy = loginInfo.Id,
                LastUpdatedTime = currentTime
            };
            var rs = _debugLogBlackListDao.Insert(item);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public ServiceResult<bool> Update(UpdateDebugLogBlackListRequest request, TRightsUser loginInfo)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var currentTime = DateTime.Now;
            var item = _debugLogBlackListDao.GetById(request.Id);
            if (item != null)
            {
                item.Content = request.Content;
                item.LastUpdatedBy = loginInfo.Id;
                item.LastUpdatedTime = DateTime.Now;

                var rs = _debugLogBlackListDao.Update(item);
                if (rs == true)
                {
                    result.ReturnCode = ReturnCodeType.Success;
                    result.Content = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 删除(支持批量)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<bool> DeleteDebugLogBlackList(DeleteDebugLogBlackListRequest request)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var rs = _debugLogBlackListDao.DeleteDebugLogBlackList(request);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 获取黑名单列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<PagingResult<GetPagingDebugLogBlackListResponse>> GetPagingBlackList(GetPagingDebugLogBlackListRequest request)
        {
            var result = new ServiceResult<PagingResult<GetPagingDebugLogBlackListResponse>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new PagingResult<GetPagingDebugLogBlackListResponse>()
            };

            var rs = _debugLogBlackListDao.GetPagingBlackList(request);
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

            return result;
        }
    }
}
