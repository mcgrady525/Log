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
using Log.DaoFactory;
using Log.Entity.Db;

namespace Log.Service.Rights
{
    /// <summary>
    /// 角色管理service
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RightsRoleService : IRightsRoleService
    {
        private static readonly IRightsRoleDao roleDao = Factory.GetRightsRoleDao();

        /// <summary>
        /// 角色列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<PagingResult<GetPagingRolesResponse>> GetPagingRoles(GetPagingRolesRequest request)
        {
            var result = new ServiceResult<PagingResult<GetPagingRolesResponse>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new PagingResult<GetPagingRolesResponse>()
            };

            var rs = roleDao.GetPagingRoles(request);
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

            return result;
        }

        /// <summary>
        /// 获取角色下的用户列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<PagingResult<GetPagingRoleUsersResponse>> GetPagingRoleUsers(GetPagingRoleUsersRequest request)
        {
            var result = new ServiceResult<PagingResult<GetPagingRoleUsersResponse>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new PagingResult<GetPagingRoleUsersResponse>()
            };

            var rs = roleDao.GetPagingRoleUsers(request);
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

            return result;
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public ServiceResult<bool> AddRole(AddRoleRequest request, TRightsUser loginInfo)
        {
            //需要检查是否已存在相同名称的角色
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var role = roleDao.GetRoleByName(request.Name);
            if (role != null)
            {
                result.Message = "已存在相同名称的角色!";
                return result;
            }

            var currentTime = DateTime.Now;
            var item = new TRightsRole
            {
                Name = request.Name,
                Description = request.Description,
                OrganizationId = request.OrgId,
                CreatedBy = loginInfo.Id,
                CreatedTime = currentTime,
                LastUpdatedBy = loginInfo.Id,
                LastUpdatedTime = currentTime
            };
            var rs = roleDao.Insert(item);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public ServiceResult<bool> EditRole(EditRoleRequest request, TRightsUser loginInfo)
        {
            //需要检查是否已存在相同名称的角色
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var role = roleDao.GetRoleByName(request.NewName);
            if (request.NewName != request.OriginalName && role != null)
            {
                result.Message = "已存在相同名称的角色!";
                return result;
            }

            var item = roleDao.GetById(request.Id);
            if (item != null)
            {
                item.Name = request.NewName;
                item.Description = request.Description;
                item.OrganizationId = request.OrgId;
                item.LastUpdatedBy = loginInfo.Id;
                item.LastUpdatedTime = DateTime.Now;

                var rs = roleDao.Update(item);
                if (rs == true)
                {
                    result.ReturnCode = ReturnCodeType.Success;
                    result.Content = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<bool> DeleteRole(DeleteRoleRequest request)
        {
            //删除角色数据
            //删除用户角色数据
            //删除角色菜单按钮数据
            //使用事务
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var rs = roleDao.DeleteRole(request);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        public ServiceResult<List<TRightsRole>> GetAllRole()
        {
            var result = new ServiceResult<List<TRightsRole>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new List<TRightsRole>()
            };

            var rs = roleDao.GetAll();
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

            return result;
        }

        /// <summary>
        /// 角色授权页面，获取角色所拥有的菜单按钮权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public ServiceResult<List<GetRoleMenuButtonResponse>> GetRoleMenuButton(int roleId)
        {
            var result = new ServiceResult<List<GetRoleMenuButtonResponse>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new List<GetRoleMenuButtonResponse>()
            };

            var rs = roleDao.GetRoleMenuButton(new List<int> { roleId });
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

            return result;
        }

        /// <summary>
        /// 为角色授权
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<bool> AuthorizeRole(AuthorizeRoleRequest request)
        {
            //先删除该角色原来拥有的菜单和按钮权限
            //再新增该角色新选择的菜单和按钮权限
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var rs = roleDao.AuthorizeRole(request);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

    }
}
