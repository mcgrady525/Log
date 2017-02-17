﻿using Log.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Tracy.Frameworks.LogClient.Entity;

namespace Log.IService
{
    /// <summary>
    /// error log服务接口
    /// </summary>
    [ServiceContract(ConfigurationName = "LogsErrorLogService.ILogsErrorLogService")]
    public interface ILogsErrorLogService
    {
        /// <summary>
        /// 插入error log
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> AddErrorLog(ErrorLog request);
    }
}