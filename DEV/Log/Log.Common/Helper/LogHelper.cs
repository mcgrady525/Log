using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Common.Helper
{
    /// <summary>
    /// 写日志helper
    /// </summary>
    public sealed partial class LogHelper
    {
        /// <summary>
        /// 写文本日志
        /// </summary>
        /// <param name="msg"></param>
        public void WriteLogs(string msg, string logFileName)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + logFileName + ".log";
            var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            var sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(msg);

            sw.Flush();
            sw.Close();
            fs.Close();
        }

    }
}
