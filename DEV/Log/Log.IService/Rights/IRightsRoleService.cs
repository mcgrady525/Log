using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Log.Entity.Common;
using Log.Entity.Db;
using Log.Entity.ViewModel;
using Tracy.Frameworks.Common.Result;

namespace Log.IService.Rights
{
    [ServiceContract(ConfigurationName = "RightsRoleService.IRightsRoleService")]
    public interface IRightsRoleService
    {
        /// <summary>
        /// 角色列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<PagingResult<GetPagingRolesResponse>> GetPagingRoles(GetPagingRolesRequest request);

        /// <summary>
        /// 获取角色下的用户列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<PagingResult<GetPagingRoleUsersResponse>> GetPagingRoleUsers(GetPagingRoleUsersRequest request);

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> AddRole(AddRoleRequest request, TRightsUser loginInfo);

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> EditRole(EditRoleRequest request, TRightsUser loginInfo);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> DeleteRole(DeleteRoleRequest request);

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<List<TRightsRole>> GetAllRole();

        /// <summary>
        /// 角色授权页面，获取角色所拥有的菜单按钮权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<List<GetRoleMenuButtonResponse>> GetRoleMenuButton(int roleId);

        /// <summary>
        /// 为角色授权
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> AuthorizeRole(AuthorizeRoleRequest request);

    }
}
