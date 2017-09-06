using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.Entity.Db;
using Tracy.Frameworks.Common.Result;
using Log.Entity.ViewModel;

namespace Log.IDao.Rights
{
    /// <summary>
    /// 用户
    /// </summary>
    public interface IRightsUserDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        bool Insert(TRightsUser item);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="item">待更新的记录</param>
        /// <returns></returns>
        bool Update(TRightsUser item);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">待删除记录的id</param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">id列表</param>
        /// <returns></returns>
        bool BatchDelete(List<int> ids);

        /// <summary>
        /// 依id查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        TRightsUser GetById(int id);

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        List<TRightsUser> GetAll();

        /// <summary>
        /// 获取用户列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PagingResult<GetPagingUsersResponse> GetPagingUsers(GetPagingUsersRequest request);

        /// <summary>
        /// 依据userId获取用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        TRightsUser GetByUserId(string userId);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool DeleteUser(DeleteUserRequest request);

        /// <summary>
        /// 为所选用户设置机构(支持批量)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool SetOrg(SetOrgRequest request);

        /// <summary>
        /// 为所选用户设置角色(支持批量)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool SetRole(SetRoleRequest request);

        /// <summary>
        /// 获取用户所拥有的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>返回角色id，可能多个</returns>
        List<int> GetRolesByUserId(int userId);

    }
}
