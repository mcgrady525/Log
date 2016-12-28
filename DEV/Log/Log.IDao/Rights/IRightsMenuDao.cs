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
    /// 菜单管理dao接口
    /// </summary>
    public interface IRightsMenuDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        bool Insert(TRightsMenu item);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="item">待更新的记录</param>
        /// <returns></returns>
        bool Update(TRightsMenu item);

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
        TRightsMenu GetById(int id);

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        List<TRightsMenu> GetAll();

        /// <summary>
        /// 获取当前菜单关联的按钮
        /// </summary>
        /// <param name="menuId">当前菜单id</param>
        /// <returns></returns>
        List<TRightsMenuButton> GetButtonsByMenuId(int menuId);

        /// <summary>
        /// 为菜单分配按钮
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool SetButton(SetButtonRequest request);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool DeleteMenu(DeleteMenuRequest request);
    }
}
