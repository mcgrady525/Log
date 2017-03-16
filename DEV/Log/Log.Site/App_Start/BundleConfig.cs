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

            //jqueryui
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Scripts/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js"));

            //easyui
            bundles.Add(new ScriptBundle("~/bundles/easyui").Include(
                        "~/Scripts/easyui-1.3.2/jquery.easyui.min.js",
                        "~/Scripts/easyui-1.3.2/easyui-lang-zh_CN.js"));

            //common
            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                        "~/Scripts/common.js"));

            //http://www.cnblogs.com/OpenCoder/p/5180325.html
            //css
            //easyui
            bundles.Add(new StyleBundle("~/Content/Css/bootstrap").Include(
                      "~/Content/easyui/bootstrap/easyui.css",
                      "~/Content/easyui/icon.css"));

            //jquery ui
            bundles.Add(new StyleBundle("~/Content/Css/jqueryui").Include("~/Content/jquery-ui-1.9.2.custom/css/base/jquery-ui-1.9.2.custom.css"));

            //Site
            bundles.Add(new StyleBundle("~/Content/Css/site").Include(
                        "~/Content/Site.css"));
        }
    }
}
