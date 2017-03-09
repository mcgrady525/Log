using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.IDao.Rights;
using Log.IDao;

namespace Log.DaoFactory
{
    public class Factory
    {
        /// <summary>
        /// 依据dao名称创建实例
        /// </summary>
        /// <param name="daoName"></param>
        /// <returns></returns>
        private static object GetInstance(string daoName, string directoryName = "")
        {
            string configName = System.Configuration.ConfigurationManager.AppSettings["Log.DaoAccess"];
            if (string.IsNullOrEmpty(configName))
            {
                throw new Exception("还没有配置Log.DaoAccess!");
            }

            StringBuilder sbClassName = new StringBuilder(configName);
            if (!string.IsNullOrEmpty(directoryName))
            {
                sbClassName.Append("." + directoryName);
            }
            sbClassName.Append("." + daoName);

            //加载程序集
            System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(configName);

            //创建指定类型的对象实例
            return assembly.CreateInstance(sbClassName.ToString());
        }

        /// <summary>
        /// 组织机构dao
        /// </summary>
        /// <returns></returns>
        public static IRightsOrganizationDao GetRightsOrganizationDao()
        {
            return GetInstance("RightsOrganizationDao", "Rights") as IRightsOrganizationDao;
        }

        /// <summary>
        /// 权限相关dao
        /// </summary>
        /// <returns></returns>
        public static IRightsAccountDao GetRightsAccountDao()
        {
            return GetInstance("RightsAccountDao", "Rights") as IRightsAccountDao;
        }

        /// <summary>
        /// 用户dao
        /// </summary>
        /// <returns></returns>
        public static IRightsUserDao GetRightsUserDao()
        {
            return GetInstance("RightsUserDao", "Rights") as IRightsUserDao;
        }

        /// <summary>
        /// 角色dao
        /// </summary>
        /// <returns></returns>
        public static IRightsRoleDao GetRightsRoleDao()
        {
            return GetInstance("RightsRoleDao", "Rights") as IRightsRoleDao;
        }

        /// <summary>
        /// 菜单dao
        /// </summary>
        /// <returns></returns>
        public static IRightsMenuDao GetRightsMenuDao()
        {
            return GetInstance("RightsMenuDao", "Rights") as IRightsMenuDao;
        }

        /// <summary>
        /// 按钮dao
        /// </summary>
        /// <returns></returns>
        public static IRightsButtonDao GetRightsButtonDao()
        {
            return GetInstance("RightsButtonDao", "Rights") as IRightsButtonDao;
        }

        /// <summary>
        /// debug log
        /// </summary>
        /// <returns></returns>
        public static ILogsDebugLogDao GetLogsDebugLogDao()
        {
            return GetInstance("LogsDebugLogDao") as ILogsDebugLogDao;
        }

        /// <summary>
        /// error log
        /// </summary>
        /// <returns></returns>
        public static ILogsErrorLogDao GetLogsErrorLogDao()
        {
            return GetInstance("LogsErrorLogDao") as ILogsErrorLogDao;
        }

        /// <summary>
        /// xml log
        /// </summary>
        /// <returns></returns>
        public static ILogsXmlLogDao GetLogsXmlLogDao()
        {
            return GetInstance("LogsXmlLogDao") as ILogsXmlLogDao;
        }

        /// <summary>
        /// perf log
        /// </summary>
        /// <returns></returns>
        public static ILogsPerformanceLogDao GetLogsPerformanceLogDao()
        {
            return GetInstance("LogsPerformanceLogDao") as ILogsPerformanceLogDao;
        }

    }
}
