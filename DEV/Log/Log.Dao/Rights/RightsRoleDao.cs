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
    public class RightsRoleDao : IRightsRoleDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        public bool Insert(TRightsRole item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"INSERT INTO dbo.t_rights_role VALUES (@Name ,@Description ,@OrganizationId ,@CreatedBy ,@CreatedTime ,@LastUpdatedBy ,@LastUpdatedTime);", item);
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
        public bool Update(TRightsRole item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"UPDATE dbo.t_rights_role SET name= @Name, description= @Description, organization_id= @OrganizationId, last_updated_by= @LastUpdatedBy,
                    last_updated_time= @LastUpdatedTime WHERE id= @Id;", item);
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
                var effectRows = conn.Execute(@"DELETE FROM dbo.t_rights_role WHERE id= @Id;", new { @Id = id });
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
                var effectRows = conn.Execute(@"DELETE FROM dbo.t_rights_role WHERE id IN @Ids;", new { @Ids = ids });
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
        public TRightsRole GetById(int id)
        {
            TRightsRole result = null;

            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TRightsRole>(@"SELECT r.organization_id AS OrganizationId, r.created_by AS CreatedBy,r.created_time AS CreatedTime,
                    r.last_updated_by AS LastUpdatedBy,r.last_updated_time AS LastUpdatedTime,* 
                    FROM dbo.t_rights_role AS r
                    WHERE r.id= @Id;", new { @Id = id }).FirstOrDefault();
            }

            return result;
        }

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<TRightsRole> GetAll()
        {
            List<TRightsRole> result = null;

            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TRightsRole>(@"SELECT r.organization_id AS OrganizationId, r.created_by AS CreatedBy,r.created_time AS CreatedTime,
                    r.last_updated_by AS LastUpdatedBy,r.last_updated_time AS LastUpdatedTime,* 
                    FROM dbo.t_rights_role AS r").ToList();
            }

            return result;
        }

        /// <summary>
        /// 角色列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagingResult<GetPagingRolesResponse> GetPagingRoles(GetPagingRolesRequest request)
        {
            PagingResult<GetPagingRolesResponse> result = null;
            List<int> orgIds = null;
            var totalCount = 0;
            var startIndex = (request.PageIndex - 1) * request.PageSize + 1;
            var endIndex = request.PageIndex * request.PageSize;

            //默认获取所有角色(不跟机构关联)
            if (request.OrgId == 0)
            {
                using (var conn = DapperHelper.CreateConnection())
                {
                    var multi = conn.QueryMultiple(@"--获取所有角色并分页
                        SELECT  rs.*
                        FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY r.created_time DESC ) AS RowNum ,
					                        r.id,
					                        r.name,
					                        r.description,
                                            r.organization_id AS OrgId ,
                                            r.created_by AS CreatedBy ,
                                            r.created_time AS CreatedTime ,
                                            r.last_updated_by AS LastUpdatedBy ,
                                            r.last_updated_time AS LastUpdatedTime ,
                                            org.name AS OrgName
                                  FROM      dbo.t_rights_role AS r
                                  LEFT JOIN dbo.t_rights_organization AS org ON r.organization_id= org.id
                                ) AS rs
                        WHERE   rs.RowNum BETWEEN @Start AND @End;

                        --获取所有角色total
                        SELECT COUNT(DISTINCT r.id) FROM dbo.t_rights_role AS r;", new { @Start = startIndex, @End = endIndex });
                    var query1 = multi.Read<GetPagingRolesResponse>();
                    var query2 = multi.Read<int>();
                    totalCount = query2.First();

                    result = new PagingResult<GetPagingRolesResponse>(totalCount, request.PageIndex, request.PageSize, query1);
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
                    var multi = conn.QueryMultiple(@"--获取指定机构(包括所有子机构)的角色并分页
                        SELECT  rs.*
                        FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY r.created_time DESC ) AS RowNum ,
					                        r.id,
					                        r.name,
					                        r.description,
                                            r.organization_id AS OrgId ,
                                            r.created_by AS CreatedBy ,
                                            r.created_time AS CreatedTime ,
                                            r.last_updated_by AS LastUpdatedBy ,
                                            r.last_updated_time AS LastUpdatedTime ,
                                            org.name AS OrgName
                                  FROM      dbo.t_rights_role AS r
                                  LEFT JOIN dbo.t_rights_organization AS org ON r.organization_id= org.id
                                  WHERE r.organization_id IN @OrgIds
                                ) AS rs
                        WHERE   rs.RowNum BETWEEN @Start AND @End;

                        --获取指定机构(包括所有子机构)的角色total
                        SELECT COUNT(DISTINCT r.id) FROM dbo.t_rights_role AS r
                        WHERE r.organization_id IN @OrgIds;", new { @OrgIds = orgIds, @Start = startIndex, @End = endIndex });

                    var query1 = multi.Read<GetPagingRolesResponse>();
                    var query2 = multi.Read<int>();
                    totalCount = query2.First();

                    result = new PagingResult<GetPagingRolesResponse>(totalCount, request.PageIndex, request.PageSize, query1);
                }
            }

            return result;
        }

        /// <summary>
        /// 获取角色下的用户列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagingResult<GetPagingRoleUsersResponse> GetPagingRoleUsers(GetPagingRoleUsersRequest request)
        {
            PagingResult<GetPagingRoleUsersResponse> result = null;
            var totalCount = 0;
            var startIndex = (request.PageIndex - 1) * request.PageSize + 1;
            var endIndex = request.PageIndex * request.PageSize;

            using (var conn = DapperHelper.CreateConnection())
            {
                var multi = conn.QueryMultiple(@"--获取指定角色下的所有用户(分页)
                    SELECT  rs.*
                    FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY u.created_time DESC ) AS RowNum ,
                                        u.id ,
                                        u.user_id AS UserId ,
                                        u.user_name AS UserName ,
                                        u.is_change_pwd AS IsChangePwd ,
                                        u.enable_flag AS EnableFlag ,
                                        u.created_by AS CreatedBy ,
                                        u.created_time AS CreatedTime ,
                                        u.last_updated_by AS LastUpdatedBy ,
                                        u.last_updated_time AS LastUpdatedTime
                              FROM      dbo.t_rights_user AS u
                                        LEFT JOIN dbo.t_rights_user_role AS userRole ON u.id = userRole.user_id
                              WHERE     userRole.role_id = @RoleId
                            ) AS rs
                    WHERE   rs.RowNum BETWEEN @Start AND @End;

                    --获取指定角色下的所有用户total
                    SELECT  COUNT(DISTINCT u.id)
                    FROM    dbo.t_rights_user AS u
                            LEFT JOIN dbo.t_rights_user_role AS userRole ON u.id = userRole.user_id
                    WHERE   userRole.role_id = @RoleId;", new { @RoleId = request.RoleId, @Start = startIndex, @End = endIndex });
                var query1 = multi.Read<GetPagingRoleUsersResponse>();
                var query2 = multi.Read<int>();
                totalCount = query2.First();

                result = new PagingResult<GetPagingRoleUsersResponse>(totalCount, request.PageIndex, request.PageSize, query1);
            }

            return result;
        }

        /// <summary>
        /// 依据角色名获取角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns>不存在返回Null</returns>
        public TRightsRole GetRoleByName(string roleName)
        {
            TRightsRole result = null;

            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TRightsRole>(@"SELECT * FROM dbo.t_rights_role AS r WHERE r.name= @RoleName;", new { @RoleName = roleName }).FirstOrDefault();
            }

            return result;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool DeleteRole(DeleteRoleRequest request)
        {
            //删除角色数据
            //删除用户角色数据
            //删除角色菜单按钮数据
            //使用事务
            var result = false;
            using (var conn = DapperHelper.CreateConnection())
            {
                var trans = conn.BeginTransaction();

                try
                {
                    //删除角色
                    conn.Execute(@"DELETE FROM dbo.t_rights_role WHERE id= @RoleId;", new { @RoleId = request.DeleteRoleId }, trans);

                    //删除用户角色
                    conn.Execute(@"DELETE FROM dbo.t_rights_user_role WHERE role_id= @RoleId;", new { @RoleId = request.DeleteRoleId }, trans);

                    //删除角色菜单按钮
                    conn.Execute(@"DELETE FROM dbo.t_rights_role_menu_button WHERE role_id= @RoleId;", new { @RoleId = request.DeleteRoleId }, trans);

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
        /// 获取角色所拥有的菜单按钮权限，角色授权页面和首页我的信息/我的权限页面使用
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<GetRoleMenuButtonResponse> GetRoleMenuButton(List<int> roleIds)
        {
            List<GetRoleMenuButtonResponse> result = null;

            using (var conn = DapperHelper.CreateConnection())
            {
                var multi = conn.QueryMultiple(@"SELECT  menu.id AS MenuId ,
                            menu.name AS MenuName ,
                            menu.parent_id AS MenuParentId ,
                            menu.icon AS MenuIcon ,
                            button.id AS ButtonId ,
                            button.name AS ButtonName ,
                            button.icon AS ButtonIcon ,
                            roleMenuButton.role_id AS RoleId ,
                            CASE WHEN ISNULL(roleMenuButton.button_id, 0) = 0 THEN 'false'
                                 ELSE 'true'
                            END Checked ,
                            *
                    FROM    dbo.t_rights_menu AS menu
                            LEFT JOIN dbo.t_rights_menu_button AS menuButton ON menu.id = menuButton.menu_id
                            LEFT JOIN dbo.t_rights_button AS button ON menuButton.button_id = button.id
                            LEFT JOIN dbo.t_rights_role_menu_button AS roleMenuButton ON ( roleMenuButton.menu_id = menu.id
                                                                                  AND roleMenuButton.button_id = button.id
                                                                                  AND roleMenuButton.role_id IN @RoleIds
                                                                                  )
                    ORDER BY menu.parent_id ,
                            menu.sort ,
                            button.sort;", new { @RoleIds = roleIds });
                result = multi.Read<GetRoleMenuButtonResponse>().ToList();
            }

            return result;
        }

        /// <summary>
        /// 为角色授权
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool AuthorizeRole(AuthorizeRoleRequest request)
        {
            var result = false;
            var addRoleMenuButtons = new List<TRightsRoleMenuButton>();

            //没有为该角色授权
            if (request.MenuButtonId.IsNullOrEmpty())
            {
                using (var conn = DapperHelper.CreateConnection())
                {
                    conn.Execute(@"DELETE FROM dbo.t_rights_role_menu_button WHERE role_id= @RoleId AND button_id!=0;", new { @RoleId = request.RoleId });
                    result = true;
                    return result;
                }
            }

            //有为该角色授权
            var menuButtons = request.MenuButtonId.Trim(new char[] { '|' }).Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (menuButtons.HasValue())
            {
                foreach (var menuButton in menuButtons)
                {
                    var menuButtonArr = menuButton.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    addRoleMenuButtons.Add(new TRightsRoleMenuButton
                    {
                        RoleId = request.RoleId,
                        MenuId = menuButtonArr[0].ToInt(),
                        ButtonId = menuButtonArr[1].ToInt()
                    });
                }

                using (var conn = DapperHelper.CreateConnection())
                {
                    var trans = conn.BeginTransaction();

                    try
                    {
                        //先删除(不能删除buttonId=0的记录，否则登陆后无法访问顶级菜单)
                        conn.Execute(@"DELETE FROM dbo.t_rights_role_menu_button WHERE role_id= @RoleId AND button_id! = 0;", new { @RoleId = request.RoleId }, trans);

                        //后添加
                        conn.Execute(@"INSERT INTO dbo.t_rights_role_menu_button VALUES  ( @RoleId,@MenuId,@ButtonId);", addRoleMenuButtons, trans);

                        trans.Commit();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                    }
                }
            }

            return result;
        }

    }
}
