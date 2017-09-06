using Log.Entity.Db;
using Log.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracy.Frameworks.Common.Result;

namespace Log.IDao.Rights
{
    /// <summary>
    /// 按钮管理dao接口
    /// </summary>
    public interface IRightsButtonDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        bool Insert(TRightsButton item);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="item">待更新的记录</param>
        /// <returns></returns>
        bool Update(TRightsButton item);

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
        TRightsButton GetById(int id);

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        List<TRightsButton> GetAll();

        /// <summary>
        /// 获取所有按钮(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PagingResult<GetPagingButtonsResponse> GetPagingButtons(GetPagingButtonsRequest request);

        /// <summary>
        /// 依名称查询按钮，不存在返回NULL
        /// </summary>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        TRightsButton GetButtonByName(string buttonName);

        /// <summary>
        /// 依标识码查询按钮，不存在返回NULL
        /// </summary>
        /// <param name="buttonCode"></param>
        /// <returns></returns>
        TRightsButton GetButtonByCode(string buttonCode);

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool DeleteButton(DeleteButtonRequest request);

    }
}
