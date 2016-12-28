using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.IDao.Rights;
using Log.Entity.Db;
using Tracy.Frameworks.Common.Extends;
using Tracy.Frameworks.Common.Helpers;
using Dapper;
using Log.Common.Helper;
using Log.Entity.ViewModel;

namespace Log.Dao.Rights
{
    /// <summary>
    /// 菜单管理dao
    /// </summary>
    public class RightsMenuDao : IRightsMenuDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        public bool Insert(TRightsMenu item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectedRows = conn.Execute(@"INSERT INTO dbo.t_rights_menu VALUES  ( @Name ,@ParentId ,@Code ,@Url ,@Icon ,@Sort ,@CreatedBy ,@CreatedTime ,@LastUpdatedBy ,@LastUpdatedTime);", item);
                if (effectedRows > 0)
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
        public bool Update(TRightsMenu item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectedRows = conn.Execute(@"UPDATE dbo.t_rights_menu SET name=@Name, url= @Url, icon= @Icon, sort=@Sort, last_updated_by= @LastUpdatedBy, last_updated_time= @LastUpdatedTime
                    WHERE id= @Id;", item);
                if (effectedRows > 0)
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
                var effectedRows = conn.Execute(@"DELETE FROM dbo.t_rights_menu WHERE id= @Id;", new { @Id = id });
                if (effectedRows > 0)
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
                var effectedRows = conn.Execute(@"DELETE FROM dbo.t_rights_menu WHERE id IN @Ids;", new { @Ids = ids });
                if (effectedRows > 0)
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
        public TRightsMenu GetById(int id)
        {
            var result = new TRightsMenu();
            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TRightsMenu>(@"SELECT menu.parent_id AS ParentId,
                    menu.created_by AS CreatedBy,
                    menu.created_time AS CreatedTime,
                    menu.last_updated_by AS LastUpdatedBy,
                    menu.last_updated_time AS LastUpdatedTime,
                    * FROM dbo.t_rights_menu AS menu
                    WHERE menu.id= @Id;", new { @Id = id }).FirstOrDefault();
            }

            return result;
        }

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<TRightsMenu> GetAll()
        {
            var result = new List<TRightsMenu>();
            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TRightsMenu>(@"SELECT menu.parent_id AS ParentId,
                    menu.created_by AS CreatedBy,
                    menu.created_time AS CreatedTime,
                    menu.last_updated_by AS LastUpdatedBy,
                    menu.last_updated_time AS LastUpdatedTime,
                    * FROM dbo.t_rights_menu AS menu
                    ORDER BY menu.parent_id, menu.sort;").ToList();
            }

            return result;
        }

        /// <summary>
        /// 获取当前菜单关联的按钮
        /// </summary>
        /// <param name="menuId">当前菜单id</param>
        /// <returns></returns>
        public List<TRightsMenuButton> GetButtonsByMenuId(int menuId)
        {
            var result = new List<TRightsMenuButton>();
            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TRightsMenuButton>(@"SELECT menuButton.menu_id AS MenuId, menuButton.button_id AS ButtonId,* 
                                                            FROM dbo.t_rights_menu_button AS menuButton
                                                            WHERE menuButton.menu_id= @MenuId;", new { MenuId = menuId }).ToList();
            }

            return result;
        }

        /// <summary>
        /// 为菜单分配按钮
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool SetButton(SetButtonRequest request)
        {
            //先删除原来分配的按钮,如果是删除按钮，需要同时删除角色菜单按钮中的记录
            //再增加新分配的按钮
            //使用事务
            var addMenuButtons = new List<TRightsMenuButton>();
            var delMenuButtons = new List<TRightsMenuButton>();
            var buttonIds = request.buttonIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.ToInt()).ToList();
            if (buttonIds.HasValue())
            {
                foreach (var item in buttonIds)
                {
                    var addMenuButton = new TRightsMenuButton
                    {
                        MenuId = request.MenuId,
                        ButtonId = item
                    };
                    addMenuButtons.Add(addMenuButton);
                }
            }

            var originButtons = GetButtonsByMenuId(request.MenuId).Select(p => p.ButtonId.Value).ToList();
            //var addButtonIds = buttonIds.Except(originButtons).ToList();
            var delButtonIds = originButtons.Except(buttonIds).ToList();
            if (delButtonIds.HasValue())
            {
                foreach (var item in delButtonIds)
                {
                    var delMenuButton = new TRightsMenuButton
                    {
                        MenuId = request.MenuId,
                        ButtonId = item
                    };
                    delMenuButtons.Add(delMenuButton);
                }
            }

            using (var conn = DapperHelper.CreateConnection())
            {
                var trans = conn.BeginTransaction();

                try
                {
                    //删除原来的菜单按钮
                    conn.Execute(@"DELETE FROM dbo.t_rights_menu_button WHERE menu_id= @MenuId;", new { @MenuId = request.MenuId }, trans);

                    //删除角色菜单按钮(如果有取消关联按钮的话)
                    conn.Execute(@"DELETE FROM dbo.t_rights_role_menu_button WHERE menu_id= @MenuId AND button_id= @ButtonId;", delMenuButtons, trans);

                    //新增新分配的
                    conn.Execute(@"INSERT INTO dbo.t_rights_menu_button VALUES (@MenuId,@ButtonId);", addMenuButtons, trans);

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
            }
            return false;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool DeleteMenu(DeleteMenuRequest request)
        {
            //删除菜单数据
            //删除菜单按钮数据
            //删除角色菜单按钮数据
            //使用事务
            var result = false;
            var deletedMenuIds = request.DeleteMenuIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.ToInt()).ToList();
            using (var conn = DapperHelper.CreateConnection())
            {
                var trans = conn.BeginTransaction();
                try
                {
                    //删除菜单数据
                    conn.Execute(@"DELETE FROM dbo.t_rights_menu WHERE id IN @MenuIds;", new { @MenuIds = deletedMenuIds }, trans);

                    //删除菜单按钮数据
                    conn.Execute(@"DELETE FROM dbo.t_rights_menu_button WHERE menu_id IN @MenuIds;", new { @MenuIds = deletedMenuIds }, trans);

                    //删除角色菜单按钮数据
                    conn.Execute(@"DELETE FROM dbo.t_rights_role_menu_button WHERE menu_id IN @MenuIds;", new { @MenuIds = deletedMenuIds }, trans);

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
    }
}
