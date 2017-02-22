using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Log.Common.Helper
{
    /// <summary>
    /// 配置文件扩展
    /// </summary>
    public static partial class ConfigHelper
    {
        /// <summary>
        /// 获取应用程序根目录路径
        /// </summary>
        public static readonly string BASEDIRECTORY = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// 获取AppSettings
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetAppSetting(string key)
        {
            if (ConfigurationManager.AppSettings.Count == 0 || !ConfigurationManager.AppSettings.HasKeys() || !ConfigurationManager.AppSettings.AllKeys.Any(p => p.Equals(key)))
            {
                return string.Empty;
            }

            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 获取connectionStrings
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConnectionString(string key)
        {
            if (ConfigurationManager.ConnectionStrings.Count == 0)
            {
                return string.Empty;
            }

            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

    }
}