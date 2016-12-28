using Log.DaoFactory;
using Log.Entity.Common;
using Log.Entity.ViewModel;
using Log.IDao.Rights;
using Log.IService.Rights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Tracy.Frameworks.Common.Result;
using Log.Entity.Db;
using Tracy.Frameworks.Common.Extends;

namespace Log.Service.Rights
{
    /// <summary>
    /// 按钮管理service
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RightsButtonService : IRightsButtonService
    {
        //注入dao
        private static readonly IRightsButtonDao btnDao = Factory.GetRightsButtonDao();

        /// <summary>
        /// 获取所有按钮(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<PagingResult<GetPagingButtonsResponse>> GetPagingButtons(GetPagingButtonsRequest request)
        {
            var result = new ServiceResult<PagingResult<GetPagingButtonsResponse>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new PagingResult<GetPagingButtonsResponse>()
            };

            var rs = btnDao.GetPagingButtons(request);
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

            return result;
        }

        /// <summary>
        /// 新增按钮
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<bool> AddButton(AddButtonRequest request, TRightsUser loginInfo)
        {
            //校验按钮名称和标识码
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var buttonByName = btnDao.GetButtonByName(request.Name);
            if (buttonByName != null)
            {
                result.Message = "已存在相同名称的按钮!";
                return result;
            }

            var buttonByCode = btnDao.GetButtonByCode(request.Code);
            if (buttonByCode != null)
            {
                result.Message = "已存在相同标识码的按钮!";
                return result;
            }

            var currentTime = DateTime.Now;
            var btn = new TRightsButton
            {
                Name = request.Name,
                Code = request.Code,
                Icon = request.Icon,
                Sort = request.Sort,
                CreatedBy = loginInfo.Id,
                CreatedTime = currentTime,
                LastUpdatedBy = loginInfo.Id,
                LastUpdatedTime = currentTime
            };
            var rs = btnDao.Insert(btn);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

        /// <summary>
        /// 修改按钮
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public ServiceResult<bool> EditButton(EditButtonRequest request, TRightsUser loginInfo)
        {
            //校验按钮名称
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };
            var button = btnDao.GetButtonByName(request.Name);
            if (request.Name != request.OriginalName && button != null)
            {
                result.Message = "已存在相同名称的按钮!";
                return result;
            }

            var btn = btnDao.GetById(request.Id);
            if (btn != null)
            {
                btn.Name = request.Name;
                btn.Icon = request.Icon;
                btn.Sort = request.Sort;
                btn.LastUpdatedBy = loginInfo.Id;
                btn.LastUpdatedTime = DateTime.Now;
                var rs = btnDao.Update(btn);
                if (rs == true)
                {
                    result.ReturnCode = ReturnCodeType.Success;
                    result.Content = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<bool> DeleteButton(DeleteButtonRequest request)
        {
            //删除按钮数据
            //删除菜单按钮数据
            //删除角色菜单按钮数据
            //使用事务
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var rs = btnDao.DeleteButton(request);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }

    }
}
