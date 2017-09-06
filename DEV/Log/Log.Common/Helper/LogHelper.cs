using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Common.Helper
{
    /// <summary>
    /// 写日志helper
    /// </summary>
    public static class LogHelper
    {
        private static readonly Logger defaultLog = LogManager.GetLogger("Log.Log");

        static LogHelper()
        {
            var path = ConfigurationManager.AppSettings["Nlog.Config.Path"];
            if (string.IsNullOrEmpty(path))
            {
                //兼容Task
                if (File.Exists("VConfigs\\NLog.config"))
                {
                    path = "VConfigs\\NLog.config";
                }
            }
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(filePath);
        }

        /// <summary>
        /// 写调试日志，写到本地文本文件(VLogs目录下)
        /// </summary>
        /// <param name="action"></param>
        public static void Debug(Func<string> action)
        {
            if (defaultLog.IsDebugEnabled)
            {
                defaultLog.Debug(action());
            }
        }

        /// <summary>
        /// 写Info日志，写到本地文本文件(VLogs目录下)
        /// </summary>
        /// <param name="action"></param>
        public static void Info(Func<string> action)
        {
            if (defaultLog.IsInfoEnabled)
            {
                defaultLog.Info(action());
            }
        }

        /// <summary>
        /// 写错误日志，写到本地文本文件(VLogs目录下)
        /// </summary>
        /// <param name="action"></param>
        public static void Error(Func<string> action)
        {
            if (defaultLog.IsErrorEnabled)
            {
                defaultLog.Error(action());
            }
        }

    }
}
