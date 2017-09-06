using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Log.IService.Rights;
using Log.Entity.Common;
using Tracy.Frameworks.Common.Result;
using Log.Entity.ViewModel;
using Log.IDao.Rights;
using Log.Entity.Db;
using Tracy.Frameworks.Common.Extends;
using Tracy.Frameworks.Common.Helpers;

namespace Log.Service.Rights
{
    /// <summary>
    /// 用户管理service
    /// </summary>
    public class RightsUserService : IRightsUserService
    {
        private readonly IRightsUserDao _userDao;

        public RightsUserService(IRightsUserDao userDao)
        {
            _userDao = userDao;
        }

        /// <summary>
        /// 获取用户列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns>分页结果集</returns>
        public ServiceResult<PagingResult<GetPagingUsersResponse>> GetPagingUsers(GetPagingUsersRequest request)
        {
            var result = new ServiceResult<PagingResult<GetPagingUsersResponse>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new PagingResult<GetPagingUsersResponse>()
            };

            var rs = _userDao.GetPagingUsers(request);
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

            return result;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public ServiceResult<bool> AddUser(AddUserRequest request, TRightsUser loginInfo)
        {
            //新增用户前需要检查userId是否存在
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var existUser = _userDao.GetByUserId(request.UserId);
            if (existUser != null)
            {
                result.Message = "已存在该用户,请更换其它用户id!";
                return result;
            }

            var currentTime = DateTime.Now;
            var item = new TRightsUser
            {
                UserId = request.UserId,
                Password = EncryptHelper.MD5With32bit("123456"),//默认密码为123456
                UserName = request.UserName,
                IsChangePwd = request.IsChangePwd,
                EnableFlag = request.EnableFlag,
                CreatedBy = loginInfo.Id,
                CreatedTime = currentTime,
                LastUpdatedBy = loginInfo.Id,
                LastUpdatedTime = currentTime
            };
            var rs = _userDao.Insert(item);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public ServiceResult<bool> EditUser(EditUserRequest request, TRightsUser loginInfo)
        {
            //先要检查新的userId是否已经存在，不存在才能继续修改
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var user = _userDao.GetByUserId(request.NewUserId);
            if (request.NewUserId != request.OriginalUserId && user != null)
            {
                result.Message = "已存在该用户,请更换其它用户id!";
                return result;
            }

            var item = _userDao.GetById(request.Id);
            if (item != null)
            {
                item.UserId = request.NewUserId;
                item.UserName = request.NewUserName;
                item.EnableFlag = request.EnableFlag;
                item.IsChangePwd = request.IsChangePwd;
                item.LastUpdatedBy = loginInfo.Id;
                item.LastUpdatedTime = DateTime.Now;

                var rs = _userDao.Update(item);
                if (rs == true)
                {
                    result.ReturnCode = ReturnCodeType.Success;
                    result.Content = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<bool> DeleteUser(DeleteUserRequest request)
        {
            //删除用户表数据
            //解除用户-机构的关系
            //解除用户-角色的关系
            //需要使用事务
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var rs = _userDao.DeleteUser(request);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 为所选用户设置机构
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<bool> SetOrg(SetOrgRequest request)
        {
            //先删除所选用户原来的所属机构
            //再新增所选用户选择的所属机构
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var rs = _userDao.SetOrg(request);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 为所选用户设置角色(支持批量)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<bool> SetRole(SetRoleRequest request)
        {
            //先删除所选用户原来的拥有角色
            //再新增所选用户选择的新角色
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var rs = _userDao.SetRole(request);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }
    }
}
