using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Optimization;

namespace MVCTemplate
{
    public class BundleConfig
    {
        public class AsIsBundleOrderer : IBundleOrderer
        {

            public virtual IEnumerable<FileInfo> OrderFiles(BundleContext context, IEnumerable<FileInfo> files)
            {
                return files;
            }

            public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
            {
                return files;
            }
        }
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //              "~/Scripts/js/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                        "~/Scripts/js/jquery-3.3.1.min.js",
                        "~/Content/assets/plugins/modernizr.custom.js",
                        "~/Content/datatables/datatables.min.js",
                        "~/Content/datatables/dataTables.buttons.min.js",
                        "~/Content/datatables/Buttons-1.3.1/js/buttons.flash.min.js",
                        "~/Content/datatables/JSZip-3.1.3/jszip.min.js",
                        "~/Content/datatables/pdfmake-0.1.27/build/pdfmake.min.js",
                        "~/Content/datatables/vfs_fonts.js",
                        "~/Content/datatables/buttons.html5.min.js",
                        "~/Content/datatables/buttons.print.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/datatables/datatables.min.css",
                "~/Content/datatables/buttons.dataTables.min.css",
                "~/Content/assets/plugins/pace/pace-theme-flash.css",
                "~/Content/assets/plugins/bootstrap/css/bootstrap.min.css",
                "~/Content/assets/plugins/font-awesome/css/fontawesome-all.min.css",
                "~/Content/assets/plugins/jquery-scrollbar/jquery.scrollbar.css",
                "~/Content/assets/plugins/select2/css/select2.min.css",
                "~/Content/assets/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css",
                "~/Content/assets/plugins/switchery/css/switchery.min.css",
                "~/Content/pages/css/pages-icons.css",
                "~/Content/pages/css/pages.css",
                "~/Content/custom/styles.css",
                "~/Content/assets/plugins/jquery-dynatree/skin/ui.dynatree.css"
                ));

            var bundle = new Bundle("~/Scripts/plugins");
            bundle.Orderer = new AsIsBundleOrderer();
            bundle.Include(
                "~/Content/assets/plugins/pace/pace.min.js",
                "~/Content/assets/plugins/jquery-ui/jquery-ui.min.js",
                "~/Content/assets/plugins/tether/js/tether.min.js",
                "~/Content/assets/plugins/bootstrap/js/bootstrap.min.js",
                "~/Content/assets/plugins/select2/js/select2.full.min.js",
                "~/Content/assets/plugins/jquery-scrollbar/jquery.scrollbar.min.js",
                "~/Content/assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js",
                "~/Content/assets/plugins//boostrap-form-wizard/js/jquery.bootstrap.wizard.min.js",
                "~/Content/assets/plugins/jquery-validation/js/jquery.validate.min.js",
                "~/Content/datatables/datatables.min.js",
                "~/Content/assets/plugins/jquery-dynatree/jquery.dynatree.min.js",
                "~/Content/assets/plugins/classie/classie.js"
                );
            bundles.Add(bundle);

            bundles.Add(new ScriptBundle("~/Scripts/pages").
                Include("~/Content/pages/js/pages.min.js")
                .Include("~/Content/assets/js/scripts.js"));

            //bundles.Add(new ScriptBundle("~/Scripts/angular").Include("~/Scripts/angular.js"));
            bundles.Add(new ScriptBundle("~/Scripts/modalform").Include("~/Scripts/custom/modalform.min.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}
