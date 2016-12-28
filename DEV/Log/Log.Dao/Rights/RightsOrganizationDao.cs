using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.Entity.Db;
using Log.IDao.Rights;
using Log.Common.Helper;
using Dapper;
using Tracy.Frameworks.Common.Extends;
using Log.Entity.ViewModel;

namespace Log.Dao.Rights
{
    /// <summary>
    /// 组织机构数据访问Dao
    /// </summary>
    public class RightsOrganizationDao : IRightsOrganizationDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        public bool Insert(TRightsOrganization item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"INSERT INTO dbo.t_rights_organization VALUES  ( @OrgName ,@ParentId ,@Code ,@OrganizationType ,@Sort ,@EnableFlag ,@CreatedBy ,@CreatedTime ,@LastUpdatedBy ,@LastUpdatedTime);",
                                new
                                {
                                    @OrgName = item.Name,
                                    @ParentId = item.ParentId,
                                    @Code = item.Code,
                                    @OrganizationType = item.OrganizationType,
                                    @Sort = item.Sort,
                                    @EnableFlag = item.EnableFlag,
                                    @CreatedBy = item.CreatedBy,
                                    @CreatedTime = item.CreatedTime,
                                    @LastUpdatedBy = item.LastUpdatedBy,
                                    @LastUpdatedTime = item.LastUpdatedTime
                                });
                if (effectRows > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="item">待更新的记录</param>
        /// <returns></returns>
        public bool Update(TRightsOrganization item)
        {
            var result = false;
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"UPDATE dbo.t_rights_organization SET name= @OrgName, parent_id= @ParentId, sort= @Sort, last_updated_by= @LastUpdatedBy, last_updated_time= @LastUpdatedTime WHERE id= @Id;",
                                             new { @Id = item.Id, @OrgName = item.Name, @ParentId = item.ParentId, @Sort = item.Sort, @LastUpdatedBy = item.LastUpdatedBy, @LastUpdatedTime = item.LastUpdatedTime });
                if (effectRows > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">待删除记录的id</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var result = false;
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"DELETE FROM dbo.t_rights_organization WHERE id= @Id;", new { @Id = id });
                if (effectRows > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">id列表</param>
        /// <returns></returns>
        public bool BatchDelete(List<int> ids)
        {
            var result = false;
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"DELETE FROM dbo.t_rights_organization WHERE id IN @Ids;", new { @Ids = ids });
                if (effectRows > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 依id查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public TRightsOrganization GetById(int id)
        {
            TRightsOrganization result = null;
            using (var conn = DapperHelper.CreateConnection())
            {
                var query = conn.Query<TRightsOrganization>(@"SELECT org.id AS Id, org.name AS NAME, org.parent_id AS ParentId, org.code AS Code, org.organization_type AS OrganizationType,
                                                            org.sort AS Sort, org.enable_flag AS EnableFlag, org.created_by AS CreatedBy, org.created_time AS CreatedTime,
                                                            org.last_updated_by AS LastUpdatedBy, org.last_updated_time AS LastUpdatedTime 
                                                            FROM dbo.t_rights_organization AS org
                                                            WHERE org.id= @Id", new { @Id = id });
                result = query.FirstOrDefault();
            }

            return result;
        }

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<TRightsOrganization> GetAll()
        {
            List<TRightsOrganization> result = null;
            using (var conn = DapperHelper.CreateConnection())
            {
                var query = conn.Query<TRightsOrganization>(@"SELECT org.id AS Id, org.name AS NAME, org.parent_id AS ParentId, org.code AS Code, org.organization_type AS OrganizationType,
                                                            org.sort AS Sort, org.enable_flag AS EnableFlag, org.created_by AS CreatedBy, org.created_time AS CreatedTime,
                                                            org.last_updated_by AS LastUpdatedBy, org.last_updated_time AS LastUpdatedTime 
                                                            FROM dbo.t_rights_organization AS org
                                                            ORDER BY org.code, org.sort;");
                result = query.ToList();
            }

            return result;
        }

        /// <summary>
        /// 获取当前用户当前页面可以访问的按钮列表
        /// </summary>
        /// <param name="menuCode"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<TRightsButton> GetButtonsByUserIdAndMenuCode(string menuCode, int userId)
        {
            List<TRightsButton> result = null;
            using (var conn = DapperHelper.CreateConnection())
            {
                var query = conn.Query<TRightsButton, TRightsRoleMenuButton, TRightsMenu, TRightsUserRole, TRightsUser, TRightsButton>(@"SELECT * FROM dbo.t_rights_button AS button
                    LEFT JOIN dbo.t_rights_role_menu_button AS roleMenuButton ON button.id= roleMenuButton.button_id
                    LEFT JOIN dbo.t_rights_menu AS menu ON roleMenuButton.menu_id= menu.id
                    LEFT JOIN dbo.t_rights_user_role AS userRole ON userRole.role_id= roleMenuButton.role_id
                    LEFT JOIN dbo.t_rights_user AS u ON u.id= userRole.user_id
                    WHERE u.id= @UserId AND menu.code= @MenuCode;", (button, roleMenuButton, menu, userRole, user) =>
                                                                  {
                                                                      return button;
                                                                  },
                                                                  new
                                                                  {
                                                                      @UserId = userId,
                                                                      @MenuCode = menuCode
                                                                  }).ToList();
                if (query.HasValue())
                {
                    result = query.DistinctBy(p => p.Id).OrderBy(p => p.Sort).ToList();
                }
            }

            return result;
        }

        /// <summary>
        /// 获取指定机构的所有子机构，包含当前机构
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<TRightsOrganization> GetChildrenOrgs(int orgId)
        {
            List<TRightsOrganization> result = new List<TRightsOrganization>();
            if (orgId == 0)//获取全部
            {
                result = RecursionChildrenOrgs(orgId).ToList();
            }
            else//当前机构+所有子机构
            {
                var currentOrg = GetById(orgId);
                result.Add(currentOrg);

                var childrenOrgs = RecursionChildrenOrgs(orgId);
                result.AddRange(childrenOrgs);
            }

            return result;
        }

        /// <summary>
        /// 删除机构(支持批量)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool DeleteOrganization(DeleteOrganizationRequest request)
        {
            //删除机构
            //同时解除用户-机构关系
            //使用事务
            var result = false;
            var orgIds = request.DeleteOrgIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.ToInt()).ToList();
            using (var conn = DapperHelper.CreateConnection())
            {
                var trans = conn.BeginTransaction();
                try
                {
                    //删除机构
                    conn.Execute(@"DELETE FROM dbo.t_rights_organization WHERE id IN @Ids;", new { @Ids = orgIds }, trans);

                    //删除用户-机构
                    conn.Execute(@"DELETE FROM dbo.t_rights_user_organization WHERE organization_id IN @OrgIds;", new { @OrgIds = orgIds }, trans);

                    trans.Commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
            }

            return result;
        }

        #region Private method

        /// <summary>
        /// 递归获取指定机构的所有子机构
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IEnumerable<TRightsOrganization> RecursionChildrenOrgs(int parentId)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var parentOrgs = conn.Query<TRightsOrganization>(@"SELECT org.parent_id AS ParentId,org.organization_type AS OrganizationType, org.enable_flag AS EnableFlag,
                    org.created_by AS CreatedBy, org.created_time AS CreatedTime, org.last_updated_by AS LastUpdatedBy, org.last_updated_time AS LastUpdatedTime,* 
                    FROM dbo.t_rights_organization AS org
                    WHERE org.enable_flag= 1 AND org.parent_id= @ParentId
                    ORDER BY org.code, org.sort;", new { @ParentId = parentId });

                return parentOrgs.ToList().Concat(parentOrgs.ToList().SelectMany(p => RecursionChildrenOrgs(p.Id)));
            }
        }

        #endregion

    }
}
