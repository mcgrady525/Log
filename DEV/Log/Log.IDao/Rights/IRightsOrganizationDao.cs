using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.Entity.Db;
using Log.Entity.ViewModel;

namespace Log.IDao.Rights
{
    /// <summary>
    /// 组织机构数据访问Dao接口
    /// </summary>
    public interface IRightsOrganizationDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        bool Insert(TRightsOrganization item);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="item">待更新的记录</param>
        /// <returns></returns>
        bool Update(TRightsOrganization item);

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
        TRightsOrganization GetById(int id);

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        List<TRightsOrganization> GetAll();

        /// <summary>
        /// 获取当前用户当前页面可以访问的按钮列表
        /// </summary>
        /// <param name="menuCode"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<TRightsButton> GetButtonsByUserIdAndMenuCode(string menuCode, int userId);

        /// <summary>
        /// 获取指定机构的所有子机构，包含当前机构
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TRightsOrganization> GetChildrenOrgs(int orgId);

        /// <summary>
        /// 删除机构(支持批量)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool DeleteOrganization(DeleteOrganizationRequest request);

    }
}
