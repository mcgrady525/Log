using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.Entity.Db;
using Log.Entity.ViewModel;
using Log.Entity.Rights;

namespace Log.IDao.Rights
{
    /// <summary>
    /// 登陆相关
    /// </summary>
    public interface IRightsAccountDao
    {
        /// <summary>
        /// 检查登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns>成功返回实体对象，失败返回null</returns>
        TRightsUser CheckLogin(CheckLoginRequest request);

        /// <summary>
        /// 获取指定父菜单下的所有子菜单
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="menuParentId">菜单parentId</param>
        /// <returns></returns>
        List<TRightsMenu> GetAllChildrenMenu(int userId, int menuParentId);

        /// <summary>
        /// 首次登录初始化密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool InitUserPwd(FirstLoginRequest request, TRightsUser loginInfo);

        /// <summary>
        /// 首页我的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GetMyInfoResponse GetMyInfo(int id);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool ChangePwd(ChangePwdRequest request, TRightsUser loginInfo);
    }
}
