using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.Attributes
{
    /// <summary>
    /// 表示该类与数据库中的表或视图对应
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TableAttribute : Attribute
    {
        public TableAttribute()
        {
        }

        public TableAttribute(string tableName)
        {
            TableName = tableName;
        }

        public TableAttribute(string tableName, bool isReadonly)
        {
            TableName = tableName;
            IsReadOnly = isReadonly;
        }

        public string TableName { get; set; }

        /// <summary>
        /// 表示只能读取数据，而不能增加，修改和删除（暂时没有用）
        /// </summary>
        public bool IsReadOnly { get; set; }
    }
}
