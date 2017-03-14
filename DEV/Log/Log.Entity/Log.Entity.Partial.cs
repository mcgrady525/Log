using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Log.Entity.Db;
using System.Runtime.Serialization;

//数据库实体的扩展属性，不存在于数据库中，只是为了扩展，最好写明其用处。
namespace Log.Entity.Db
{
    /// <summary>
    /// TRightsMenu的扩展属性
    /// </summary>
    public partial class TRightsMenu
    {
        /// <summary>
        /// [扩展属性，数据库不存在]菜单关联的按钮，有可能多个
        /// </summary>
        [DataMember]
        public TRightsButton Button { get; set; }

        /// <summary>
        /// [扩展属性，数据库不存在]角色授权页面使用
        /// </summary>
        [DataMember]
        public int RoleId { get; set; }

        /// <summary>
        /// [扩展属性，数据库不存在]角色授权页面使用
        /// </summary>
        [DataMember]
        public bool Checked { get; set; }
    }

    public partial class TLogsDebugLog
    {
        /// <summary>
        /// Message字段解压
        /// </summary>
        [DataMember]
        public string MessageDetail { get; set; }

        /// <summary>
        /// Detail字段解压
        /// </summary>
        [DataMember]
        public string LogDetail { get; set; }

    }

    public partial class TLogsErrorLog
    {
        /// <summary>
        /// Message字段解压
        /// </summary>
        [DataMember]
        public string MessageDetail { get; set; }

        /// <summary>
        /// Detail字段解压
        /// </summary>
        [DataMember]
        public string LogDetail { get; set; }

    }
}
