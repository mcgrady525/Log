using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Tracy.Frameworks.Common.Extends;

namespace Log.Common.Helper
{
    /// <summary>
    /// Dapper帮助类
    /// </summary>
    public sealed partial class DapperHelper
    {
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <returns></returns>
        public static IDbConnection CreateConnection()
        {
            IDbConnection conn = null;
            var connStr = ConfigHelper.GetConnectionString("LogDB");
            conn = new SqlConnection(connStr);

            var isMiniProfilerEnabled = ConfigHelper.GetAppSetting("IsMiniProfilerEnabled").ToBool();
            if (isMiniProfilerEnabled)
            {
                conn = new ProfiledDbConnection(new SqlConnection(connStr), MiniProfiler.Current);
            }

            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }

            return conn;
        }
    }
}
