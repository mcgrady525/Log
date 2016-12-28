using Log.Entity.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Log.Entity.Common;
using Log.Entity.ViewModel;

namespace Log.IService.Rights
{
    /// <summary>
    /// 组织机构service接口
    /// </summary>
    [ServiceContract(ConfigurationName = "RightsOrganizationService.IRightsOrganizationService")]
    public interface IRightsOrganizationService
    {
        /// <summary>
        /// 插入机构
        /// </summary>
        /// <param name="item">待插入的记录</param>
        [OperationContract]
        ServiceResult<bool> Insert(TRightsOrganization item);

        /// <summary>
        /// 更新机构
        /// </summary>
        /// <param name="item">待更新的记录</param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> Update(TRightsOrganization item);

        /// <summary>
        /// 删除机构
        /// </summary>
        /// <param name="id">待删除记录的id</param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> Delete(int id);

        /// <summary>
        /// 依id查询机构
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<TRightsOrganization> GetById(int id);

        /// <summary>
        /// 获取所有机构
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<List<TRightsOrganization>> GetAll();

        /// <summary>
        /// 获取当前用户当前页面可以访问的按钮列表
        /// </summary>
        /// <param name="menuCode">菜单code</param>
        /// <param name="pageName"></param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<List<TRightsButton>> GetButtonsByUserIdAndMenuCode(string menuCode, int userId);

        /// <summary>
        /// 获取指定机构的所有子机构，0表示获取所有，包含当前机构
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<List<TRightsOrganization>> GetChildrenOrgs(int orgId);

        /// <summary>
        /// 新增机构
        /// </summary>
        /// <param name="request">request</param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> AddOrganization(AddOrganizationRequest request, TRightsUser loginInfo);

        /// <summary>
        /// 修改机构
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> EditOrganization(EditOrganizationRequest request, TRightsUser loginInfo);

        /// <summary>
        /// 删除机构(支持批量删除)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> DeleteOrganization(DeleteOrganizationRequest request);


    }
}
