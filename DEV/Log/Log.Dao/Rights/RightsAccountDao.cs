using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.IDao.Rights;
using Log.Entity.Db;
using Log.Entity.ViewModel;
using Log.Common.Helper;
using Dapper;
using Log.Entity.Rights;
using Tracy.Frameworks.Common.Extends;
using Tracy.Frameworks.Common.Consts;

namespace Log.Dao.Rights
{
    /// <summary>
    /// 登陆相关
    /// </summary>
    public class RightsAccountDao : IRightsAccountDao
    {
        /// <summary>
        /// 当前用户可以访问的所有菜单
        /// </summary>
        private static List<TRightsMenu> UserMenus = new List<TRightsMenu>();

        /// <summary>
        /// 检查登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns>成功返回实体对象，失败返回null</returns>
        public TRightsUser CheckLogin(CheckLoginRequest request)
        {
            TRightsUser user = null;
            using (var conn = DapperHelper.CreateConnection())
            {
                user = conn.Query<TRightsUser>(@"SELECT u.id, u.user_id AS UserId, u.password, u.user_name AS UserName, u.is_change_pwd AS IsChangePwd, u.enable_flag AS EnableFlag,
                                        u.created_by AS CreatedBy, u.created_time AS CreatedTime, u.last_updated_by AS LastUpdatedBy, u.last_updated_time AS LastUpdatedTime
                                        FROM dbo.t_rights_user AS u
                                        WHERE u.user_id= @UserId AND u.password= @Password;", new { @UserId = request.loginId, @Password = request.loginPwd }).FirstOrDefault();
            }

            return user;
        }

        /// <summary>
        /// 获取指定父菜单下的所有子菜单
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="menuParentId">菜单parentId</param>
        /// <returns></returns>
        public List<TRightsMenu> GetAllChildrenMenu(int userId, int menuParentId)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var query = conn.Query<TRightsMenu, TRightsRoleMenuButton, TRightsUserRole, TRightsUser, TRightsMenu>(@"SELECT menu.id, menu.name, menu.parent_id AS ParentId, menu.code, menu.url, menu.icon,menu.sort,
                menu.created_by AS CreatedBy, menu.created_time AS CreatedTime,
                menu.last_updated_by AS LastUpdatedBy, menu.last_updated_time AS LastUpdatedTime,* 
                FROM dbo.t_rights_menu AS menu
                LEFT JOIN dbo.t_rights_role_menu_button AS roleMenuButton ON menu.id= roleMenuButton.menu_id
                LEFT JOIN dbo.t_rights_user_role AS userRole ON roleMenuButton.role_id = userRole.role_id
                LEFT JOIN dbo.t_rights_user AS u ON userRole.user_id = u.id
                WHERE u.id= @UserId
                ORDER BY menu.parent_id, menu.sort;", (menu, roleMenuButton, userRole, user) =>
                {
                    return menu;
                }, new { @UserId = userId });

                UserMenus = query.DistinctBy(p => p.Id).ToList();
            }

            return RecursionAllChildrenMenu(menuParentId).ToList();
        }

        /// <summary>
        /// 递归获取所有子菜单
        /// </summary>
        /// <param name="menuParentId"></param>
        /// <returns></returns>
        public IEnumerable<TRightsMenu> RecursionAllChildrenMenu(int menuParentId)
        {
            var query = from item in UserMenus
                        where item.ParentId == menuParentId
                        select item;
            if (query != null && query.Count() > 0 && menuParentId == 0)
            {
                return query;
            }

            return query.ToList().Concat(query.ToList().SelectMany(p => RecursionAllChildrenMenu(p.Id)));
        }

        /// <summary>
        /// 首次登录初始化密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool InitUserPwd(FirstLoginRequest request, TRightsUser loginInfo)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                //查询
                var user = conn.Query<TRightsUser>(@"SELECT u.user_id AS UserId, u.user_name AS UserName, u.is_change_pwd AS IsChangePwd, u.enable_flag AS EnableFlag, u.created_by AS CreatedBy,
                u.created_time AS CreatedTime, u.last_updated_by AS LastUpdatedBy, u.last_updated_time AS LastUpdatedTime,* 
                FROM dbo.t_rights_user AS u
                WHERE u.id= @Id;", new { @Id = request.Id }).FirstOrDefault();
                if (user != null)
                {
                    //更新
                    var effectRows = conn.Execute(@"UPDATE dbo.t_rights_user SET is_change_pwd= 1, password= @Password, last_updated_by= @LastUpdatedBy, last_updated_time= @LastUpdatedTime WHERE id= @Id;",
                        new
                        {
                            @Password = request.NewPwd,
                            @Id = request.Id,
                            @LastUpdatedBy = loginInfo.Id,
                            @LastUpdatedTime = DateTime.Now
                        });
                    if (effectRows > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 首页我的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GetMyInfoResponse GetMyInfo(int id)
        {
            GetMyInfoResponse result = null;
            using (var conn = DapperHelper.CreateConnection())
            {
                var users = conn.Query<TRightsUser, TRightsUserOrganization, TRightsOrganization, TRightsUserRole, TRightsRole, TRightsUser>(@"SELECT u.user_id AS UserId, u.user_name AS UserName, u.created_time AS CreatedTime,* 
                    FROM dbo.t_rights_user AS u
                    LEFT JOIN dbo.t_rights_user_organization AS userOrg ON u.id= userOrg.user_id
                    LEFT JOIN dbo.t_rights_organization AS org ON userOrg.organization_id= org.id
                    LEFT JOIN dbo.t_rights_user_role AS userRole ON u.id= userRole.user_id
                    LEFT JOIN dbo.t_rights_role AS r ON userRole.role_id= r.id
                    WHERE u.id= @Id;", (user, userOrg, org, userRole, role) =>
                                     {
                                         user.Organization = org;
                                         user.Role = role;
                                         return user;
                                     }, new { @Id = id }).ToList();
                if (users.HasValue())
                {
                    result = new GetMyInfoResponse
                    {
                        UserId = users.First().UserId,
                        UserName = users.First().UserName,
                        CreatedTime = users.First().CreatedTime.ToString(DateTimeTypeConst.DATETIME),
                        RolesName = users.First().Role != null ? string.Join(",", users.Select(p => p.Role.Name).Distinct()) : "",
                        DepartmentsName = users.First().Organization != null ? string.Join(",", users.Select(p => p.Organization.Name).Distinct()) : ""
                    };
                }
            }

            return result;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool ChangePwd(ChangePwdRequest request, TRightsUser loginInfo)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var user = conn.Query<TRightsUser>(@"SELECT u.user_id AS UserId, u.user_name AS UserName, u.is_change_pwd AS IsChangePwd, u.enable_flag AS EnableFlag, u.created_by AS CreatedBy,
                    u.created_time AS CreatedTime, u.last_updated_by AS LastUpdatedBy, u.last_updated_time AS LastUpdatedTime,* 
                    FROM dbo.t_rights_user AS u
                    WHERE u.id= @Id;", new { @Id = request.Id }).FirstOrDefault();
                if (user != null)
                {
                    var effectRows = conn.Execute(@"UPDATE dbo.t_rights_user SET password= @NewPwd, last_updated_by= @LastUpdatedBy, last_updated_time= @LastUpdatedTime WHERE id= @Id;",
                        new
                        {
                            @Id = request.Id,
                            @NewPwd = request.NewPwd,
                            @LastUpdatedBy = loginInfo.Id,
                            @LastUpdatedTime = DateTime.Now
                        });
                    if (effectRows > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
