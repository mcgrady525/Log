using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Log.Entity.Common
{
    /// <summary>
    /// 封装service返回结果
    /// </summary>
    [DataContract, Serializable]
    public class ServiceResult<T>
    {
        /// <summary>
        /// 返回单个实体
        /// </summary>
        [DataMember]
        public T Content { get; set; }

        /// <summary>
        /// 返回集合
        /// </summary>
        [DataMember]
        public List<T> Contents { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// 數據庫返回記錄數
        /// </summary>
        [DataMember]
        public int RecordCount { get; set; }

        /// <summary>
        /// 返回枚舉
        /// 常用 未定義的結果：Unknown = 0, 成功：Success = 1, 出錯：Error = 2 
        /// </summary>
        [DataMember]
        public ReturnCodeType ReturnCode { get; set; }

        /// <summary>
        /// 返回值
        /// 目前用做存儲 SessionKey
        /// </summary>
        [DataMember]
        public string Item1 { get; set; }
    }
}
