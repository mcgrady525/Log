using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracy.Frameworks.Common.Extends;

namespace Log.Common.Helper
{
    /// <summary>
    /// 配置文件扩展，AppSettings
    /// </summary>
    public sealed partial class ConfigHelper
    {
        /// <summary>
        /// 批量写入数据库最大数，默认为100
        /// </summary>
        public static int LogMaxPostCount
        {
            get
            {
                return GetAppSetting("Log.MaxPostCount").ToInt(100);
            }
        }

        /// <summary>
        /// 间隔多少ms写一次数据库，默认为1000ms
        /// </summary>
        public static int LogInsertCycleTime
        {
            get
            {
                return GetAppSetting("Log.InsertCycleTime").ToInt(1000);
            }
        }


    }
}
