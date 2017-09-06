using Log.Entity.Db;
using Log.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracy.Frameworks.Common.Result;

namespace Log.IDao
{
    /// <summary>
    /// error log黑名单dao接口
    /// </summary>
    public interface ILogsErrorLogBlackListDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        bool Insert(TLogsErrorLogBlackList item);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="item">待更新的记录</param>
        /// <returns></returns>
        bool Update(TLogsErrorLogBlackList item);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">待删除记录的id</param>
        /// <returns></returns>
        bool Delete(long id);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">id列表</param>
        /// <returns></returns>
        bool BatchDelete(List<long> ids);

        /// <summary>
        /// 依id查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        TLogsErrorLogBlackList GetById(long id);

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        List<TLogsErrorLogBlackList> GetAll();

        /// <summary>
        /// 获取黑名单列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PagingResult<GetPagingErrorLogBlackListResponse> GetPagingBlackList(GetPagingErrorLogBlackListRequest request);

        /// <summary>
        /// 删除黑名单(支持批量)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool DeleteErrorLogBlackList(DeleteErrorLogBlackListRequest request);

    }
}
