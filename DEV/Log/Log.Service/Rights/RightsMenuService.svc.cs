using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.IService.Rights;
using System.ServiceModel.Activation;
using System.ServiceModel;
using Log.Entity.Db;
using Log.Entity.Common;
using Log.IDao.Rights;
using Tracy.Frameworks.Common.Extends;
using Log.Entity.ViewModel;

namespace Log.Service.Rights
{
    /// <summary>
    /// 菜单管理service
    /// </summary>
    public class RightsMenuService : IRightsMenuService
    {
        //注入dao
        private readonly IRightsMenuDao _menuDao;
        private readonly IRightsButtonDao _buttonDao;

        public RightsMenuService(IRightsMenuDao menuDao, IRightsButtonDao buttonDao)
        {
            _menuDao = menuDao;
            _buttonDao = buttonDao;
        }

        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        public ServiceResult<List<TRightsMenu>> GetAll()
        {
            var result = new ServiceResult<List<TRightsMenu>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new List<TRightsMenu>()
            };

            var rs = _menuDao.GetAll();
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

            return result;
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public ServiceResult<bool> AddMenu(AddMenuRequest request, TRightsUser loginInfo)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var currentTime = DateTime.Now;
            var menu = new TRightsMenu
            {
                Name = request.Name,
                ParentId = request.ParentId,
                Code = request.Code,
                Url = request.Url,
                Icon = request.Icon,
                Sort = request.Sort,
                CreatedBy = loginInfo.Id,
                CreatedTime = currentTime,
                LastUpdatedBy = loginInfo.Id,
                LastUpdatedTime = currentTime
            };
            var rs = _menuDao.Insert(menu);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public ServiceResult<bool> EditMenu(EditMenuRequest request, TRightsUser loginInfo)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var menu = _menuDao.GetById(request.Id);
            if (menu != null)
            {
                menu.Name = request.Name;
                menu.Url = request.Url;
                menu.Icon = request.Icon;
                menu.Sort = request.Sort;
                menu.LastUpdatedBy = loginInfo.Id;
                menu.LastUpdatedTime = DateTime.Now;
                var rs = _menuDao.Update(menu);
                if (rs == true)
                {
                    result.ReturnCode = ReturnCodeType.Success;
                    result.Content = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<bool> DeleteMenu(DeleteMenuRequest request)
        {
            //删除菜单数据
            //删除菜单-按钮数据
            //删除角色菜单按钮数据
            //使用事务
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var rs = _menuDao.DeleteMenu(request);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 获取当前菜单关联的按钮列表
        /// </summary>
        /// <param name="menuId">当前菜单id</param>
        /// <returns></returns>
        public ServiceResult<List<GetButtonResponse>> GetButton(string menuId)
        {
            //首先获取所有按钮
            //融合当前菜单关联的按钮
            var result = new ServiceResult<List<GetButtonResponse>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new List<GetButtonResponse>()
            };

            var allButtons = _buttonDao.GetAll();
            var menuButtons = _menuDao.GetButtonsByMenuId(menuId.ToInt());
            if (allButtons.HasValue())
            {
                foreach (var item in allButtons)
                {
                    var getButtonResponse = new GetButtonResponse
                    {
                        MenuId = menuId.ToInt(),
                        ButtonId = item.Id,
                        ButtonName = item.Name,
                        ButtonIcon = item.Icon
                    };
                    if (menuButtons.HasValue() && menuButtons.Any(p => p.ButtonId == item.Id))
                    {
                        getButtonResponse.IsChecked = true;
                    }

                    result.Content.Add(getButtonResponse);
                }
            }
            result.ReturnCode = ReturnCodeType.Success;

            return result;
        }

        /// <summary>
        /// 为菜单分配按钮
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<bool> SetButton(SetButtonRequest request)
        {
            //先删除原来分配的按钮
            //再增加新分配的按钮
            //使用事务
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var rs = _menuDao.SetButton(request);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }
    }
}
