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
    /// <summary>
    /// 用户管理service
    /// </summary>
    [ServiceContract(ConfigurationName = "RightsUserService.IRightsUserService")]
    public interface IRightsUserService
    {
        /// <summary>
        /// 获取用户列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns>分页结果集</returns>
        [OperationContract]
        ServiceResult<PagingResult<GetPagingUsersResponse>> GetPagingUsers(GetPagingUsersRequest request);

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> AddUser(AddUserRequest request, TRightsUser loginInfo);

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> EditUser(EditUserRequest request, TRightsUser loginInfo);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> DeleteUser(DeleteUserRequest request);

        /// <summary>
        /// 为所选用户设置机构
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> SetOrg(SetOrgRequest request);

        /// <summary>
        /// 为所选用户设置角色(支持批量)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> SetRole(SetRoleRequest request);

    }
}
