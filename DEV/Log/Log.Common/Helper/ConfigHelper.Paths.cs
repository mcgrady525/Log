using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracy.Frameworks.Configurations;

namespace Log.Common.Helper
{
    /// <summary>
    /// 配置文件扩展，Paths
    /// </summary>
    public sealed partial class ConfigHelper
    {
        /// <summary>
        /// 一次性加载所有Paths地址
        /// </summary>
        static ConfigHelper()
        {
            var paths = System.Configuration.ConfigurationManager.GetSection("Paths") as PathsConfigurationSection;
            LogSite = paths.LogSite.Path.TrimEnd('/') + "/";
            LogOpenApiSite = paths.LogOpenApiSite.Path.TrimEnd('/') + "/";
            LogServiceSite = paths.LogServiceSite.Path.TrimEnd('/') + "/";
        }

        /// <summary>
        /// 以"/"结尾
        /// </summary>
        public static string LogSite { get; set; }

        /// <summary>
        /// 以"/"结尾
        /// </summary>
        public static string LogOpenApiSite { get; set; }

        /// <summary>
        /// 以"/"结尾
        /// </summary>
        public static string LogServiceSite { get; set; }

    }
}
