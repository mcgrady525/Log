using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.IDao.Rights;
using Log.Entity.Db;
using Log.Common.Helper;
using Dapper;
using Tracy.Frameworks.Common.Result;
using Tracy.Frameworks.Common.Extends;
using Log.Entity.ViewModel;

namespace Log.Dao.Rights
{
    /// <summary>
    /// 用户
    /// </summary>
    public class RightsUserDao : IRightsUserDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        public bool Insert(TRightsUser item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"INSERT INTO dbo.t_rights_user VALUES ( @UserId ,@Password ,@UserName ,@IsChangePwd ,@EnableFlag ,@CreatedBy ,@CreatedTime ,@LastUpdatedBy ,@LastUpdatedTime);", item);
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
        public bool Update(TRightsUser item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"UPDATE dbo.t_rights_user SET user_id= @UserId, user_name= @UserName, enable_flag= @EnableFlag, is_change_pwd= @IsChangePwd, last_updated_by= @LastUpdatedBy, last_updated_time= @LastUpdatedTime WHERE id= @Id;", item);
                if (effectRows > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">待删除记录的id</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"DELETE FROM dbo.t_rights_user WHERE id= @Id;", new { @Id = id });
                if (effectRows > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">id列表</param>
        /// <returns></returns>
        public bool BatchDelete(List<int> ids)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"DELETE FROM dbo.t_rights_user WHERE id IN @Ids;", new { @Ids = ids });
                if (effectRows > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 依id查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public TRightsUser GetById(int id)
        {
            TRightsUser result = null;
            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TRightsUser>(@"SELECT u.user_id AS UserId, u.user_name AS UserName, u.is_change_pwd AS IsChangePwd, u.enable_flag AS EnableFlag,
                    u.created_by AS CreatedBy, u.created_time AS CreatedTime, u.last_updated_by AS LastUpdatedBy, u.last_updated_time AS LastUpdatedTime,* 
                    FROM dbo.t_rights_user AS u WHERE u.id= @Id;", new { @Id = id }).FirstOrDefault();
            }

            return result;
        }

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<TRightsUser> GetAll()
        {
            List<TRightsUser> result = null;
            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TRightsUser>(@"SELECT  u.user_id AS UserId ,
                                                            u.user_name AS UserName ,
                                                            u.is_change_pwd AS IsChangePwd ,
                                                            u.enable_flag AS EnableFlag ,
                                                            u.created_by AS CreatedBy ,
                                                            u.created_time AS CreatedTime ,
                                                            u.last_updated_by AS LastUpdatedBy ,
                                                            u.last_updated_time AS LastUpdatedTime ,
                                                            *
                                                    FROM    dbo.t_rights_user AS u
                                                    ORDER BY u.id;").ToList();
            }

            return result;
        }

        /// <summary>
        /// 获取用户列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagingResult<GetPagingUsersResponse> GetPagingUsers(GetPagingUsersRequest request)
        {
            //获取当前机构包括所有子机构(如果有子机构的话)id
            //分页查询和获取总数
            PagingResult<GetPagingUsersResponse> result = null;
            List<int> orgIds = null;
            var totalCount = 0;
            var startIndex = (request.PageIndex - 1) * request.PageSize + 1;
            var endIndex = request.PageIndex * request.PageSize;

            //默认获取所有用户(不跟机构关联)
            if (request.OrgId == 0)
            {
                using (var conn = DapperHelper.CreateConnection())
                {
                    var multi = conn.QueryMultiple(@"--获取所有用户
                        SELECT  r.*
                        FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY u.created_time DESC ) AS RowNum ,
                                            u.id ,
                                            u.user_id AS UserId ,
                                            u.user_name AS UserName ,
                                            u.is_change_pwd AS IsChangePwd ,
                                            u.enable_flag AS EnableFlag ,
                                            u.created_time AS CreatedTime,
                                            u.last_updated_time AS LastUpdatedTime
                                  FROM      dbo.t_rights_user AS u
                                ) AS r
                        WHERE   r.RowNum BETWEEN @Start AND @End;

                        --获取所有用户total
                        SELECT COUNT(DISTINCT u.id) FROM dbo.t_rights_user AS u;", new { @Start = startIndex, @End = endIndex });
                    var query1 = multi.Read<GetPagingUsersResponse>();
                    var query2 = multi.Read<int>();
                    totalCount = query2.First();

                    result = new PagingResult<GetPagingUsersResponse>(totalCount, request.PageIndex, request.PageSize, query1);
                }
            }
            else
            {
                var childrenOrgs = new RightsOrganizationDao().GetChildrenOrgs(request.OrgId);
                if (childrenOrgs.HasValue())
                {
                    orgIds = childrenOrgs.DistinctBy(p => p.Id).OrderBy(p => p.Id).Select(p => p.Id).ToList();
                }

                using (var conn = DapperHelper.CreateConnection())
                {
                    var multi = conn.QueryMultiple(@"--CTE,目的distinct
                    WITH cte_paging_user AS
                    (
                        SELECT DISTINCT  u.id ,
                                u.user_id AS UserId ,
                                u.user_name AS UserName ,
                                u.is_change_pwd AS IsChangePwd ,
                                u.enable_flag AS EnableFlag ,
                                u.created_time AS CreatedTime,
                                u.last_updated_time AS LastUpdatedTime
                        FROM    dbo.t_rights_user AS u
                                LEFT JOIN dbo.t_rights_user_organization AS userOrg ON u.id = userOrg.user_id
                        WHERE   userOrg.organization_id IN @OrgIds
                    )

                    --分页
                    SELECT r.*
                    FROM    ( 
			                    SELECT ROW_NUMBER() OVER(ORDER BY cu.id) AS RowNum, cu.* FROM cte_paging_user AS cu
                            ) AS r
                    WHERE   r.RowNum BETWEEN @Start AND @End;

                    --total
                    SELECT COUNT(DISTINCT u.id)
                    FROM    dbo.t_rights_user AS u
                            LEFT JOIN dbo.t_rights_user_organization AS userOrg ON u.id = userOrg.user_id
                    WHERE   userOrg.organization_id IN @OrgIds;", new { @OrgIds = orgIds, @Start = startIndex, @End = endIndex });

                    var query1 = multi.Read<GetPagingUsersResponse>();
                    var query2 = multi.Read<int>();
                    totalCount = query2.First();

                    result = new PagingResult<GetPagingUsersResponse>(totalCount, request.PageIndex, request.PageSize, query1);
                }
            }

            //获取每个用户所属机构和所拥有的角色
            using (var conn = DapperHelper.CreateConnection())
            {
                List<TRightsOrganization> userOrgs = null;
                List<TRightsRole> userRoles = null;
                foreach (var user in result.Entities)
                {
                    userOrgs = conn.Query<TRightsOrganization, TRightsUserOrganization, TRightsUser, TRightsOrganization>(@"SELECT org.parent_id AS ParentId, org.organization_type AS OrganizationType, org.enable_flag AS EnableFlag,
                        org.created_by AS CreatedBy, org.created_time AS CreatedTime, org.last_updated_by AS LastUpdatedBy, org.last_updated_time AS LastUpdatedTime,*
                        FROM dbo.t_rights_organization AS org
                        LEFT JOIN dbo.t_rights_user_organization AS userOrg ON org.id= userOrg.organization_id
                        LEFT JOIN dbo.t_rights_user AS u ON userOrg.user_id= u.id
                        WHERE u.id= @UserId;", (org, userOrg, u) => { return org; }, new { @UserId = user.Id }).ToList();

                    userRoles = conn.Query<TRightsRole, TRightsUserRole, TRightsUser, TRightsRole>(@"SELECT r.organization_id AS OrganizationId, r.created_by AS CreatedBy, r.created_time AS CreatedTime, r.last_updated_by AS LastUpdatedBy,
                        r.last_updated_time AS LastUpdatedTime,* 
                        FROM dbo.t_rights_role AS r
                        LEFT JOIN dbo.t_rights_user_role AS userRole ON r.id= userRole.role_id
                        LEFT JOIN dbo.t_rights_user AS u ON userRole.user_id= u.id
                        WHERE u.id= @UserId;", (role, userRole, u) => { return role; }, new { @UserId = user.Id }).ToList();

                    user.UserOrgIds = string.Join(",", userOrgs.Select(p => p.Id).ToList());
                    user.UserOrgNames = string.Join(",", userOrgs.Select(p => p.Name).ToList());
                    user.UserRoleIds = string.Join(",", userRoles.Select(p => p.Id).ToList());
                    user.UserRoleNames = string.Join(",", userRoles.Select(p => p.Name).ToList());
                }
            }

            return result;
        }

        /// <summary>
        /// 依据userId获取用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>存在则返回实体对象，不存在则返回null</returns>
        public TRightsUser GetByUserId(string userId)
        {
            TRightsUser result = null;
            using (var conn = DapperHelper.CreateConnection())
            {
                var query = conn.Query<TRightsUser>(@"SELECT TOP 1 * FROM dbo.t_rights_user AS u WHERE u.user_id= @UserId;", new { @UserId = userId }).ToList();
                result = query.FirstOrDefault();
            }

            return result;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool DeleteUser(DeleteUserRequest request)
        {
            //删除用户表数据
            //解除用户-机构的关系
            //解除用户-角色的关系
            //需要使用事务
            var result = false;
            List<int> ids = request.Ids.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.ToInt()).ToList();
            using (var conn = DapperHelper.CreateConnection())
            {
                var trans = conn.BeginTransaction();
                try
                {
                    //删除用户数据
                    conn.Execute(@"DELETE FROM dbo.t_rights_user WHERE id IN @Ids;", new { @Ids = ids }, trans);

                    //删除用户-机构数据
                    conn.Execute(@"DELETE FROM dbo.t_rights_user_organization WHERE user_id IN @Ids;", new { @Ids = ids }, trans);

                    //删除用户-角色数据
                    conn.Execute(@"DELETE FROM dbo.t_rights_user_role WHERE user_id IN @Ids;", new { @Ids = ids }, trans);

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

        /// <summary>
        /// 为所选用户设置机构
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool SetOrg(SetOrgRequest request)
        {
            //先删除所选用户原来的所属机构
            //再新增所选用户选择的所属机构
            //一定要先删除再添加，而且要使用事务
            var result = false;
            var userIds = request.UserIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.ToInt()).OrderBy(p => p).ToList();
            var orgIds = request.OrgIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.ToInt()).OrderBy(p => p).ToList();
            var addUserOrgs = new List<TRightsUserOrganization>();//待添加的用户机构

            foreach (var userId in userIds)
            {
                foreach (var orgId in orgIds)
                {
                    var addUserOrg = new TRightsUserOrganization()
                    {
                        UserId = userId,
                        OrganizationId = orgId
                    };
                    addUserOrgs.Add(addUserOrg);
                }
            }

            using (var conn = DapperHelper.CreateConnection())
            {
                var trans = conn.BeginTransaction();

                try
                {
                    //先删除
                    conn.Execute(@"DELETE FROM dbo.t_rights_user_organization WHERE user_id IN @UserIds;", new { @UserIds = userIds }, trans);

                    //后添加
                    conn.Execute(@"INSERT INTO dbo.t_rights_user_organization VALUES  (@UserId,@OrganizationId);", addUserOrgs, trans);

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

        /// <summary>
        /// 为所选用户设置角色(支持批量)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool SetRole(SetRoleRequest request)
        {
            //先删除所选用户原来的拥有角色
            //再新增所选用户选择的新角色
            //使用事务
            var result = false;
            var userIds = request.UserIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.ToInt()).OrderBy(p => p).ToList();
            var roleIds = request.RoleIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.ToInt()).OrderBy(p => p).ToList();
            var addUserRoles = new List<TRightsUserRole>();//待添加的用户角色

            foreach (var userId in userIds)
            {
                foreach (var roleId in roleIds)
                {
                    var addUserRole = new TRightsUserRole()
                    {
                        UserId = userId,
                        RoleId = roleId
                    };
                    addUserRoles.Add(addUserRole);
                }
            }

            using (var conn = DapperHelper.CreateConnection())
            {
                var trans = conn.BeginTransaction();

                try
                {
                    //先删除
                    conn.Execute(@"DELETE FROM dbo.t_rights_user_role WHERE user_id IN @UserIds;", new { @UserIds = userIds }, trans);

                    //后添加
                    conn.Execute(@"INSERT INTO dbo.t_rights_user_role VALUES ( @UserId,@RoleId);", addUserRoles, trans);

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

        /// <summary>
        /// 获取用户所拥有的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>返回角色id，可能多个</returns>
        public List<int> GetRolesByUserId(int userId)
        {
            var result = new List<int>();
            using (var conn = DapperHelper.CreateConnection())
            {
                var userRoles = conn.Query<TRightsUserRole>(@"SELECT userRole.id, userRole.user_id AS UserId, userRole.role_id AS RoleId FROM dbo.t_rights_user AS u
                    LEFT JOIN dbo.t_rights_user_role AS userRole ON u.id= userRole.user_id
                    WHERE u.id= @UserId;", new { @UserId = userId }).ToList();
                result = userRoles.Select(p => p.RoleId.Value).ToList();
            }

            return result;
        }
    }
}
