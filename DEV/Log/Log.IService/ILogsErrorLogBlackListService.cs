using Log.Entity.Common;
using Log.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracy.Frameworks.Common.Result;
using Log.Entity.Db;

namespace Log.IService
{
    /// <summary>
    /// error log黑名单service接口
    /// </summary>
    public interface ILogsErrorLogBlackListService
    {
        /// <summary>
        /// 获取黑名单列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ServiceResult<PagingResult<GetPagingErrorLogBlackListResponse>> GetPagingBlackList(GetPagingErrorLogBlackListRequest request);

        /// <summary>
        /// 添加黑名单
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        ServiceResult<bool> Insert(InsertErrorLogBlackListRequest request, TRightsUser loginInfo);

        /// <summary>
        /// 删除黑名单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ServiceResult<bool> DeleteBlackList(DeleteErrorLogBlackListRequest request);


    }
}
