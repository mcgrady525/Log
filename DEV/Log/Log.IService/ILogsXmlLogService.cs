using Log.Entity.Common;
using Log.Entity.Db;
using Log.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Tracy.Frameworks.Common.Result;

namespace Log.IService
{
    /// <summary>
    /// xml log服务接口
    /// </summary>
    [ServiceContract(ConfigurationName = "LogsXmlLogService.ILogsXmlLogService")]
    public interface ILogsXmlLogService
    {
        /// <summary>
        /// 插入xml log
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> AddXmlLog(AddXmlLogRequest request);

        /// <summary>
        /// 刷新xml日志的智能提示
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> RefreshXmlLogTip();

        /// <summary>
        /// 获取所有日志(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<PagingResult<GetPagingXmlLogsResponse>> GetPagingXmlLogs(GetPagingXmlLogsRequest request);

        /// <summary>
        /// 获取智能提示数据源
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<Tuple<List<string>, List<string>, List<string>, List<string>>> GetAutoCompleteData();

        /// <summary>
        /// 依据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<TLogsXmlLog> GetXmlLogById(long id);

    }
}
