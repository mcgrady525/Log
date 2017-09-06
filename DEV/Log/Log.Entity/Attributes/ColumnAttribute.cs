using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.Attributes
{
    /// <summary>
    /// 表示与属性对应的数据库表中列的相关的信息
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ColumnAttribute : Attribute
    {
        public ColumnAttribute()
        {
        }

        public ColumnAttribute(string columnName) : this(columnName, Category.Normal)
        {
        }

        public ColumnAttribute(Category columnCategory) : this(null, columnCategory)
        {
        }

        public ColumnAttribute(string columnName, Category columnCategory)
        {
            ColumnName = columnName;
            ColumnCategory = columnCategory;
        }

        public string ColumnName { get; set; }
        public Category ColumnCategory { get; set; }

    }

    /// <summary>
    /// 表示列的类别(是否主键，主键的类型)
    /// </summary>
    public enum Category
    {
        /// <summary>
        /// 普通列,非主键
        /// </summary>
        Normal,

        /// <summary>
        /// 自动增长的主键列
        /// </summary>
        IdentityKey,

        /// <summary>
        /// 非自增的主键列
        /// </summary>
        Key,

        /// <summary>
        /// 版本标识列(只能为DateTime,int,long类型)
        /// </summary>
        Version,

        /// <summary>
        /// 只读的非主键列,在生成更新语句时，不包含此列的信息
        /// </summary>
        ReadOnly,
    }
}
