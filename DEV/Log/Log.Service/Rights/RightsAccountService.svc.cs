using Log.Entity.Common;
using Log.Entity.Db;
using Log.Entity.ViewModel;
using Log.IService.Rights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.IDao.Rights;
using Log.Entity.Rights;

namespace Log.Service.Rights
{
    /// <summary>
    /// 登陆相关的服务
    /// </summary>
    public class RightsAccountService : IRightsAccountService
    {
        //注入dao
        private readonly IRightsAccountDao _accountDao;
        private readonly IRightsUserDao _userDao;
        private readonly IRightsRoleDao _roleDao;

        public RightsAccountService(IRightsAccountDao accountDao, IRightsUserDao userDao, IRightsRoleDao roleDao)
        {
            _accountDao = accountDao;
            _userDao = userDao;
            _roleDao = roleDao;
        }

        /// <summary>
        /// 检查登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<TRightsUser> CheckLogin(CheckLoginRequest request)
        {
            var result = new ServiceResult<TRightsUser>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var user = _accountDao.CheckLogin(request);
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = user;

            return result;
        }

        /// <summary>
        /// 获取指定父菜单下的所有子菜单
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="menuParentId">菜单parentId</param>
        /// <returns></returns>
        public ServiceResult<List<TRightsMenu>> GetAllChildrenMenu(int userId, int menuParentId)
        {
            var result = new ServiceResult<List<TRightsMenu>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new List<TRightsMenu>()
            };

            var menus = _accountDao.GetAllChildrenMenu(userId, menuParentId);
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = menus;

            return result;
        }

        /// <summary>
        /// 首次登录初始化密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<bool> InitUserPwd(FirstLoginRequest request, TRightsUser loginInfo)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            if (_accountDao.InitUserPwd(request, loginInfo))
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 首页我的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResult<GetMyInfoResponse> GetMyInfo(int id)
        {
            var result = new ServiceResult<GetMyInfoResponse>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new GetMyInfoResponse()
            };

            var myInfo = _accountDao.GetMyInfo(id);
            if (myInfo != null)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = myInfo;
            }

            return result;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<bool> ChangePwd(ChangePwdRequest request, TRightsUser loginInfo)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            if (_accountDao.ChangePwd(request, loginInfo))
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 获取当前用户的权限信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ServiceResult<List<GetRoleMenuButtonResponse>> GetMyAuthority(int userId)
        {
            //获取当前用户所拥有的所有角色(可能多个角色)
            //获取角色关联的角色菜单按钮信息
            var result = new ServiceResult<List<GetRoleMenuButtonResponse>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new List<GetRoleMenuButtonResponse>()
            };

            var roleIds = _userDao.GetRolesByUserId(userId);
            var rs = _roleDao.GetRoleMenuButton(roleIds);
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

            return result;
        }

    }
}
