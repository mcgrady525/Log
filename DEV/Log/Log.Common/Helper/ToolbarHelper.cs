using System;
using System.Data;
using System.Text;

namespace Log.Common.Helper
{
    /// <summary>
    /// Easyui Datagrid/Treegrid Toolbar帮助类
    /// </summary>
    public class ToolbarHelper
    {
        /// <summary>
        /// 输出操作按钮
        /// </summary>
        /// <param name="dt">根据用户id和菜单标识码得到的用户可以操作的此菜单下的按钮集合</param>
        /// <param name="pageName">当前页面名称，方便拼接js函数名</param>
        public static string GetToolBar(DataTable dt, string pageName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"toolbar\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (dt.Rows[i]["Code"].ToString())
                {
                    case "add"://新增
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"" + pageName + "_add();\"},");
                        break;
                    case "edit"://修改
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"" + pageName + "_edit();\"},");
                        break;
                    case "delete"://删除
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"" + pageName + "_delete();\"},");
                        break;
                    case "setorg"://设置机构
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"" + pageName + "_setorg();\"},");
                        break;
                    case "setrole"://设置角色
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"" + pageName + "_setrole();\"},");
                        break;
                    case "authorize"://角色授权
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"" + pageName + "_authorize();\"},");
                        break;
                    case "export"://导出
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"" + pageName + "_export();\"},");
                        break;
                    case "setbutton"://设置按钮
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"" + pageName + "_setbutton();\"},");
                        break;
                    case "expandall"://全部展开
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"" + pageName + "_expandall();\"},");
                        break;
                    case "collapseall"://全部折叠
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"" + pageName + "_collapseall();\"},");
                        break;
                    case "refresh_debuglog_tip"://刷新调试日志智能提示
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"" + pageName + "_refresh_debuglog_tip();\"},");
                        break;
                    case "refresh_errorlog_tip"://刷新错误日志智能提示
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"" + pageName + "_refresh_errorlog_tip();\"},");
                        break;
                    case "refresh_xmllog_tip"://刷新xml日志智能提示
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"" + pageName + "_refresh_xmllog_tip();\"},");
                        break;
                    case "refresh_perflog_tip"://刷新性能日志智能提示
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"" + pageName + "_refresh_perflog_tip();\"},");
                        break;
                    case "refresh_operatelog_tip"://刷新操作日志智能提示
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"" + pageName + "_refresh_operatelog_tip();\"},");
                        break;
                    default:
                        //browser不是按钮
                        break;
                }
            }

            bool flag = true;   //是否有浏览权限
            DataRow[] row = dt.Select("code = 'browser'");
            if (row.Length == 0)  //没有浏览权限
            {
                flag = false;
                if (dt.Rows.Count > 0)
                    sb.Remove(sb.Length - 1, 1);
            }
            else
            {
                if (dt.Rows.Count > 1)
                    sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("],\"success\":true,");
            if (flag)
                sb.Append("\"browser\":true}");
            else
                sb.Append("\"browser\":false}");

            return sb.ToString();
        }

    }
}
