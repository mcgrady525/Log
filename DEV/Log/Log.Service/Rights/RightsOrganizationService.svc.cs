using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.IService.Rights;
using System.ServiceModel.Activation;
using System.ServiceModel;
using Log.Entity.Db;
using Log.IDao.Rights;
using Log.DaoFactory;
using Log.Entity.Common;
using Tracy.Frameworks.Common.Extends;
using Log.Entity.ViewModel;

namespace Log.Service.Rights
{
    /// <summary>
    /// 组织机构service
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RightsOrganizationService : IRightsOrganizationService
    {
        private static readonly IRightsOrganizationDao orgDao = Factory.GetRightsOrganizationDao();

        /// <summary>
        /// 插入机构
        /// </summary>
        /// <param name="item">待插入的记录</param>
        public ServiceResult<bool> Insert(TRightsOrganization item)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var rs = orgDao.Insert(item);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 更新机构
        /// </summary>
        /// <param name="item">待更新的记录</param>
        /// <returns></returns>
        public ServiceResult<bool> Update(TRightsOrganization item)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var rs = orgDao.Update(item);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 删除机构
        /// </summary>
        /// <param name="id">待删除记录的id</param>
        /// <returns></returns>
        public ServiceResult<bool> Delete(int id)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var rs = orgDao.Delete(id);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 依id查询机构
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public ServiceResult<TRightsOrganization> GetById(int id)
        {
            var result = new ServiceResult<TRightsOrganization>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new TRightsOrganization()
            };

            var rs = orgDao.GetById(id);
            if (rs != null)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = rs;
            }

            return result;
        }

        /// <summary>
        /// 获取所有机构
        /// </summary>
        /// <returns></returns>
        public ServiceResult<List<TRightsOrganization>> GetAll()
        {
            var result = new ServiceResult<List<TRightsOrganization>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new List<TRightsOrganization>()
            };

            var rs = orgDao.GetAll();
            if (rs.HasValue())
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = rs;
            }

            return result;
        }

        /// <summary>
        /// 获取当前用户当前页面可以访问的按钮列表
        /// </summary>
        /// <param name="menuCode">菜单code</param>
        /// <param name="pageName"></param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public ServiceResult<List<TRightsButton>> GetButtonsByUserIdAndMenuCode(string menuCode, int userId)
        {
            var result = new ServiceResult<List<TRightsButton>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new List<TRightsButton>()
            };

            var rs = orgDao.GetButtonsByUserIdAndMenuCode(menuCode, userId);
            if (rs.HasValue())
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = rs;
            }

            return result;
        }

        /// <summary>
        /// 获取指定机构的所有子机构，0表示获取所有，包含当前机构
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public ServiceResult<List<TRightsOrganization>> GetChildrenOrgs(int orgId)
        {
            var result = new ServiceResult<List<TRightsOrganization>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new List<TRightsOrganization>()
            };

            var rs = orgDao.GetChildrenOrgs(orgId);
            result.Content = rs;
            result.ReturnCode = ReturnCodeType.Success;

            return result;
        }

        /// <summary>
        /// 新增机构
        /// </summary>
        /// <param name="request">request</param>
        /// <returns></returns>
        public ServiceResult<bool> AddOrganization(AddOrganizationRequest request, TRightsUser loginInfo)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var currentTime = DateTime.Now;
            var item = new TRightsOrganization
            {
                Name = request.Name,
                ParentId = request.ParentId,
                Code = request.Code,
                Sort = request.Sort,
                EnableFlag = true,
                CreatedBy = loginInfo.Id,
                CreatedTime = currentTime,
                LastUpdatedBy = loginInfo.Id,
                LastUpdatedTime = currentTime
            };
            var rs = orgDao.Insert(item);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 修改机构
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public ServiceResult<bool> EditOrganization(EditOrganizationRequest request, TRightsUser loginInfo)
        {
            //先查询出来再修改
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var org = orgDao.GetById(request.Id);
            if (org != null)
            {
                org.Id = request.Id;
                org.Name = request.Name;
                org.Sort = request.Sort;
                org.ParentId = request.ParentId;
                org.LastUpdatedBy = loginInfo.Id;
                org.LastUpdatedTime = DateTime.Now;
                var rs = orgDao.Update(org);
                if (rs == true)
                {
                    result.ReturnCode = ReturnCodeType.Success;
                    result.Content = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 删除机构(支持批量删除)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<bool> DeleteOrganization(DeleteOrganizationRequest request)
        {
            //删除机构
            //解除用户和机构关系
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var rs = orgDao.DeleteOrganization(request);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }
    }
}
