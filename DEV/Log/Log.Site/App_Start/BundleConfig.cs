using System.Web;
using System.Web.Optimization;

namespace Log.Site
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //js
            //jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.form.js"));

            //easyui
            bundles.Add(new ScriptBundle("~/bundles/easyui").Include(
                        "~/Scripts/easyui-1.3.2/jquery.easyui.min.js",
                        "~/Scripts/easyui-1.3.2/easyui-lang-zh_CN.js"));

            //common
            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                        "~/Scripts/common.js"));

            //css
            //easyui
            bundles.Add(new StyleBundle("~/Content/easyui/bootstrap").Include(
                      "~/Content/easyui/bootstrap/easyui.css",
                      "~/Content/easyui/icon.css"));

            //Site
            bundles.Add(new StyleBundle("~/Content/site").Include(
                        "~/Content/Site.css"));
        }
    }
}
