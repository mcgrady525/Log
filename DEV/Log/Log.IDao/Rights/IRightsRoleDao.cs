using Log.Entity.Db;
using Log.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracy.Frameworks.Common.Result;

namespace Log.IDao.Rights
{
    /// <summary>
    /// 角色管理dao
    /// </summary>
    public interface IRightsRoleDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        bool Insert(TRightsRole item);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="item">待更新的记录</param>
        /// <returns></returns>
        bool Update(TRightsRole item);

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
        TRightsRole GetById(int id);

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        List<TRightsRole> GetAll();

        /// <summary>
        /// 角色列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PagingResult<GetPagingRolesResponse> GetPagingRoles(GetPagingRolesRequest request);

        /// <summary>
        /// 获取角色下的用户列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PagingResult<GetPagingRoleUsersResponse> GetPagingRoleUsers(GetPagingRoleUsersRequest request);

        /// <summary>
        /// 依据角色名获取角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns>不存在返回Null</returns>
        TRightsRole GetRoleByName(string roleName);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool DeleteRole(DeleteRoleRequest request);

        /// <summary>
        /// 获取角色所拥有的菜单按钮权限，角色授权页面和首页我的信息/我的权限页面使用
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<GetRoleMenuButtonResponse> GetRoleMenuButton(List<int> roleIds);

        /// <summary>
        /// 为角色授权
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool AuthorizeRole(AuthorizeRoleRequest request);

    }
}
