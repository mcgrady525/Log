using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Log.Entity.Common;
using Log.Entity.Db;
using Log.Entity.ViewModel;

namespace Log.IService.Rights
{
    /// <summary>
    /// 菜单管理service接口
    /// </summary>
    [ServiceContract(ConfigurationName = "RightsMenuService.IRightsMenuService")]
    public interface IRightsMenuService
    {
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<List<TRightsMenu>> GetAll();

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> AddMenu(AddMenuRequest request, TRightsUser loginInfo);

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> EditMenu(EditMenuRequest request, TRightsUser loginInfo);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> DeleteMenu(DeleteMenuRequest request);

        /// <summary>
        /// 获取当前菜单关联的按钮列表
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<List<GetButtonResponse>> GetButton(string menuId);

        /// <summary>
        /// 为菜单分配按钮
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> SetButton(SetButtonRequest request);

    }
}
