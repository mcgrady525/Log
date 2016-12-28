using Log.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracy.Frameworks.Common.Extends;

namespace Log.Common.Helper
{
    /// <summary>
    /// Rights帮助类
    /// </summary>
    public static partial class RightsHelper
    {
        /// <summary>
        /// 获取该角色所拥有的菜单按钮权限json字符串
        /// 目前先固定成三级菜单
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetRoleMenuButtonStr(List<GetRoleMenuButtonResponse> list, int roleId = 0)
        {
            StringBuilder sb = new StringBuilder();
            var parentMenus = list.Where(p => p.MenuParentId == 0).ToList();
            sb.Append("[");
            if (parentMenus.HasValue())
            {
                for (int i = 0; i < parentMenus.Count; i++)//一级菜单
                {
                    sb.Append("{\"id\":\"" + parentMenus[i].MenuId.ToString() + "\",\"text\":\"" + parentMenus[i].MenuName + "\",\"children\":[");
                    var secondMenus = list.Where(p => p.MenuParentId == parentMenus[i].MenuId).ToList();
                    if (secondMenus.HasValue())
                    {
                        for (int j = 0; j < secondMenus.Count; j++)//二级菜单
                        {
                            sb.Append("{\"id\":\"" + secondMenus[j].MenuId.ToString() + "\",\"text\":\"" + secondMenus[j].MenuName + "\",\"children\":[");
                            var threeMenus = list.Where(p => p.MenuParentId == secondMenus[j].MenuId).ToList();
                            threeMenus = threeMenus.DistinctBy(p => p.MenuId).ToList();//distinct，因为一个menu可能有多个按钮
                            if (threeMenus.HasValue())
                            {
                                for (int k = 0; k < threeMenus.Count; k++)//三级菜单
                                {
                                    sb.Append("{\"id\":\"" + threeMenus[k].MenuId.ToString() + "\",\"text\":\"" + threeMenus[k].MenuName + "\",\"children\":[");
                                    var buttons = list.Where(p => p.MenuId == threeMenus[k].MenuId).ToList();
                                    if (buttons.HasValue())
                                    {
                                        for (int l = 0; l < buttons.Count; l++)//按钮
                                        {
                                            sb.Append("{\"id\":\"" + roleId + "\",\"text\":\"" + buttons[l].ButtonName + "\",\"checked\":" + buttons[l].Checked + ",\"attributes\":{\"menuid\":\"" + buttons[l].MenuId.ToString() + "\",\"buttonid\":\"" + buttons[l].ButtonId.ToString() + "\"}},");
                                        }
                                        sb.Remove(sb.Length - 1, 1);
                                        sb.Append("]},");
                                    }
                                    else
                                    {
                                        sb.Append("]},");
                                    }
                                }
                                sb.Remove(sb.Length - 1, 1);
                                sb.Append("]},");
                            }
                            else
                            {
                                sb.Append("]},");
                            }
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("]},");
                    }
                    else
                    {
                        sb.Append("]},");
                    }
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("]");
            }
            else
            {
                sb.Append("]");
            }

            return sb.ToString();
        }

    }
}
