using Log.Entity.Common;
using Log.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Tracy.Frameworks.Common.Result;
using Log.Entity.Db;

namespace Log.IService.Rights
{
    /// <summary>
    /// 按钮管理service接口
    /// </summary>
    [ServiceContract(ConfigurationName = "RightsButtonService.IRightsButtonService")]
    public interface IRightsButtonService
    {
        /// <summary>
        /// 获取所有按钮(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<PagingResult<GetPagingButtonsResponse>> GetPagingButtons(GetPagingButtonsRequest request);

        /// <summary>
        /// 新增按钮
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> AddButton(AddButtonRequest request, TRightsUser loginInfo);

        /// <summary>
        /// 修改按钮
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> EditButton(EditButtonRequest request, TRightsUser loginInfo);

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> DeleteButton(DeleteButtonRequest request);

    }
}
