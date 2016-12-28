using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Log.Entity.Db;
using Log.Entity.ViewModel;
using Log.Entity.Common;
using Log.Entity.Rights;

namespace Log.IService.Rights
{
    /// <summary>
    /// 登录相关的服务
    /// </summary>
    [ServiceContract(ConfigurationName = "RightsAccountService.IRightsAccountService")]
    public interface IRightsAccountService
    {
        /// <summary>
        /// 检查登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<TRightsUser> CheckLogin(CheckLoginRequest request);

        /// <summary>
        /// 获取指定父菜单下的所有子菜单
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="menuParentId">菜单parentId</param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<List<TRightsMenu>> GetAllChildrenMenu(int userId, int menuParentId);

        /// <summary>
        /// 首次登录初始化密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> InitUserPwd(FirstLoginRequest request, TRightsUser LoginInfo);

        /// <summary>
        /// 首页我的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<GetMyInfoResponse> GetMyInfo(int id);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> ChangePwd(ChangePwdRequest request, TRightsUser loginInfo);

        /// <summary>
        /// 获取当前用户的权限信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<List<GetRoleMenuButtonResponse>> GetMyAuthority(int userId);
 
    }
}
