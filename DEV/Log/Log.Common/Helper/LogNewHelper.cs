using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.Entity.Attributes;
using Tracy.Frameworks.Common.Extends;
using System.Reflection;
using Autofac;

namespace Log.Common.Helper
{
    /// <summary>
    /// 日志系统helper
    /// </summary>
    public sealed partial class LogNewHelper
    {
        /// <summary>
        /// list转DataTable
        /// 1，仅针对加了ColumnAttribute的公共属性
        /// 2，columnName处理(SystemCode->system_code)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ConvertToDataTable<T>(List<T> list)
        {
            if (list == null)
            {
                return null;
            }

            Type type = typeof(T);

            //只获取加了ColumnAttribute的公共属性
            var ps = type.GetProperties().Where(p => (ColumnAttribute)Attribute.GetCustomAttribute(p, typeof(ColumnAttribute)) != null).ToList();

            NullableConverter nullableConvert;
            List<DataColumn> cols = new List<DataColumn>();
            var columnName = string.Empty;
            Type targetType;
            foreach (var p in ps)
            {
                if (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    nullableConvert = new NullableConverter(p.PropertyType);
                    targetType = nullableConvert.UnderlyingType;
                }
                else
                {
                    targetType = p.PropertyType;
                }

                //处理columnName
                columnName = p.Name;
                var columnAttribute = Attribute.GetCustomAttribute(p, typeof(ColumnAttribute));
                if (columnAttribute != null)
                {
                    var t = columnAttribute as ColumnAttribute;
                    columnName = t.ColumnName;
                }
                cols.Add(new DataColumn(columnName, targetType));
            }

            DataTable dt = new DataTable();
            dt.Columns.AddRange(cols.ToArray());

            list.ForEach((l) =>
            {
                List<object> objs = new List<object>();
                objs.AddRange(ps.Select(p => p.GetValue(l, null)));
                dt.Rows.Add(objs.ToArray());
            });

            return dt;
        }

        /// <summary>
        /// 创建一个Autofac的Container
        /// </summary>
        /// <returns></returns>
        public static Autofac.IContainer BuildAutofacContainer()
        {
            var builder = new Autofac.ContainerBuilder();
            var iDao = Assembly.Load("Log.IDao");
            var dao = Assembly.Load("Log.Dao");
            var iService = Assembly.Load("Log.IService");
            var service = Assembly.Load("Log.Service");
            builder.RegisterAssemblyTypes(iDao, dao).Where(t => t.Name.EndsWith("Dao")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(iService, service).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();
            return builder.Build();
        }

    }
}
