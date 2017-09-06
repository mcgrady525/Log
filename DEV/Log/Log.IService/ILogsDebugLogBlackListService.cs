using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.Entity.Common;
using Log.Entity.Db;
using Log.Entity.ViewModel;
using Tracy.Frameworks.Common.Result;

namespace Log.IService
{
    /// <summary>
    /// debug log黑名单service接口
    /// </summary>
    public interface ILogsDebugLogBlackListService
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        ServiceResult<bool> Insert(InsertDebugLogBlackListRequest request, TRightsUser loginInfo);

        /// <summary>
        /// 删除(支持批量)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ServiceResult<bool> DeleteDebugLogBlackList(DeleteDebugLogBlackListRequest request);

        /// <summary>
        /// 获取黑名单列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ServiceResult<PagingResult<GetPagingDebugLogBlackListResponse>> GetPagingBlackList(GetPagingDebugLogBlackListRequest request);

    }
}
