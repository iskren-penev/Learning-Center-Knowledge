using System.Web.Optimization;

namespace LearningCenter.App
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new ScriptBundle("~/bundles/hovernavbar")
                        .Include("~/Scripts/hoverNavbar.js"));

            bundles.Add(new ScriptBundle("~/bundles/tinymce")
                        .Include("~/Scripts/tinymce/tinymce.min.js")
                        .Include("~/Scripts/tinymce/jquery.tinymce.min.js")
                        .Include("~/Scripts/tinymce/themes/modern/theme.js")
                        .Include("~/Scripts/tinymce/plugins/emoticons/plugin.js")
                        .Include("~/Scripts/tinymce/plugins/link/plugin.js")
                        .Include("~/Scripts/tinymce/plugins/textcolor/plugin.js")
                        .Include("~/Scripts/tinymce/plugins/wordcount/plugin.js"));

            bundles.Add(new StyleBundle("~/Scripts/tinymce/skins/lightgray/css")
                    .Include("~/Scripts/tinymce/skins/lightgray/skin.min.css")
                    .Include("~/Scripts/tinymce/skins/lightgray/content.min.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
