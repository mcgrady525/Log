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
    /// 按钮管理dao
    /// </summary>
    public class RightsButtonDao : IRightsButtonDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        public bool Insert(TRightsButton item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectedRows = conn.Execute(@"INSERT INTO dbo.t_rights_button VALUES (@Name ,@Code ,@Icon ,@Sort ,@CreatedBy ,@CreatedTime ,@LastUpdatedBy ,@LastUpdatedTime);", item);
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
        public bool Update(TRightsButton item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectedRows = conn.Execute(@"UPDATE dbo.t_rights_button SET name=@Name, icon= @Icon, sort=@Sort, last_updated_by= @LastUpdatedBy, last_updated_time= @LastUpdatedTime WHERE id= @Id;", item);
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
                var effectedRows = conn.Execute(@"DELETE FROM dbo.t_rights_button WHERE id= @Id;", new { @Id = id });
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
                var effectedRows = conn.Execute(@"DELETE FROM dbo.t_rights_button WHERE id IN @Ids;", new { @Ids = ids });
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
        public TRightsButton GetById(int id)
        {
            var result = new TRightsButton();
            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TRightsButton>(@"SELECT  btn.created_by AS CreatedBy ,
                                                            btn.created_time AS CreatedTime ,
                                                            btn.last_updated_by AS LastUpdatedBy ,
                                                            btn.last_updated_time AS LastUpdatedTime ,
                                                            *
                                                    FROM    dbo.t_rights_button AS btn
                                                    WHERE   btn.id = @Id;", new { @Id = id }).FirstOrDefault();
            }

            return result;
        }

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<TRightsButton> GetAll()
        {
            var result = new List<TRightsButton>();
            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TRightsButton>(@"SELECT  btn.created_by AS CreatedBy ,
                                                            btn.created_time AS CreatedTime ,
                                                            btn.last_updated_by AS LastUpdatedBy ,
                                                            btn.last_updated_time AS LastUpdatedTime ,
                                                            *
                                                    FROM    dbo.t_rights_button AS btn
                                                    ORDER BY btn.sort;").ToList();
            }

            return result;
        }

        /// <summary>
        /// 获取所有按钮(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagingResult<GetPagingButtonsResponse> GetPagingButtons(GetPagingButtonsRequest request)
        {
            PagingResult<GetPagingButtonsResponse> result = null;
            var totalCount = 0;
            var startIndex = (request.PageIndex - 1) * request.PageSize + 1;
            var endIndex = request.PageIndex * request.PageSize;

            using (var conn = DapperHelper.CreateConnection())
            {
                var multi = conn.QueryMultiple(@"--获取所有按钮(分页)
                    SELECT rs.* FROM
                    (SELECT ROW_NUMBER() OVER (ORDER BY btn.created_time DESC) AS RowNum, btn.created_time AS CreatedTime, btn.last_updated_time AS LastUpdatedTIme,* 
                    FROM dbo.t_rights_button AS btn) AS rs
                    WHERE rs.RowNum BETWEEN @Start AND @End;

                    --获取所有按钮total
                    SELECT COUNT(btn.id) FROM dbo.t_rights_button AS btn;", new { @Start = startIndex, @End = endIndex });
                var query1 = multi.Read<GetPagingButtonsResponse>();
                var query2 = multi.Read<int>();
                totalCount = query2.First();

                result = new PagingResult<GetPagingButtonsResponse>(totalCount, request.PageIndex, request.PageSize, query1);
            }

            return result;
        }

        /// <summary>
        /// 依名称查询按钮，不存在返回NULL
        /// </summary>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        public TRightsButton GetButtonByName(string buttonName)
        {
            TRightsButton result = null;
            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TRightsButton>(@"SELECT * FROM dbo.t_rights_button WHERE name= @Name;", new { @Name = buttonName }).FirstOrDefault();
            }

            return result;
        }

        /// <summary>
        /// 依标识码查询按钮，不存在返回NULL
        /// </summary>
        /// <param name="buttonCode"></param>
        /// <returns></returns>
        public TRightsButton GetButtonByCode(string buttonCode)
        {
            TRightsButton result = null;
            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TRightsButton>(@"SELECT * FROM dbo.t_rights_button WHERE code= @Code;", new { @Code = buttonCode }).FirstOrDefault();
            }

            return result;
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool DeleteButton(DeleteButtonRequest request)
        {
            //删除按钮数据
            //删除菜单按钮数据
            //删除角色菜单按钮数据
            //使用事务
            var result = false;
            var buttonId = request.DeleteButtonId.ToInt();
            using (var conn = DapperHelper.CreateConnection())
            {
                var trans = conn.BeginTransaction();
                try
                {
                    //删除按钮数据
                    conn.Execute(@"DELETE FROM dbo.t_rights_button WHERE id= @ButtonId;", new { @ButtonId = buttonId }, trans);

                    //删除菜单按钮数据
                    conn.Execute(@"DELETE FROM dbo.t_rights_menu_button WHERE button_id= @ButtonId;", new { @ButtonId = buttonId }, trans);

                    //删除角色菜单按钮数据
                    conn.Execute(@"DELETE FROM dbo.t_rights_role_menu_button WHERE button_id= @ButtonId;", new { @ButtonId = buttonId }, trans);

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
