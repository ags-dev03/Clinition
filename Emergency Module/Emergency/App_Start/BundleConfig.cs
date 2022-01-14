using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace Emergency
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*",
                "~/Scripts/moment.js",
                "~/Scripts/jquery-3.5.1.min.js",
                "~/Scripts/Jquery.signalr.js",
                "~/Scripts/popper.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/jquery-ui.min.js",
                "~/Scripts/PreventPage.js",
                "~/Scripts/jquery.alphanumeric.js",
                "~/Scripts/AlertMsg.js",
                "~/Scripts/sweetalert.min.js",
                "~/Scripts/Validation.js",
                "~/Scripts/Common.js"
             ));



            bundles.Add(new StyleBundle("~/bundles/DefCss").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/all.min.css",
                "~/Content/Common.css",
                "~/Content/Style.css",
                "~/Content/sweetalert.css",
                "~/Content/responsive.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/DashJS").Include(
                "~/Scripts/jquery-3.5.1.min.js",
                "~/assets/plugins/bootstrap/js/popper.min.js" ,
                 "~/Scripts/jquery-ui.min.js",
                "~/assets/plugins/bootstrap/js/bootstrap.min.js" ,
                   "~/Scripts/jquery.alphanumeric.js",
                "~/assets/bundles/vendorscripts.bundle.js" ,
                "~/assets/bundles/mainscripts.bundle.js" ,
                "~/assets/plugins/tables/jquery.dataTables.min.js" ,
                "~/assets/plugins/dropzone/dropzone.js" ,
                "~/assets/plugins/select2/select2.min.js" ,
                "~/assets/js/scroller.js" ,
                "~/Scripts/Jquery.signalr.js",
                //"~/Scripts/jquery-ui.min.js",
                "~/Scripts/PreventPage.js",
                "~/Scripts/AlertMsg.js",
                "~/Scripts/sweetalert.min.js",
                "~/Scripts/Validation.js",
                "~/Scripts/Common.js"
            ));



            bundles.Add(new StyleBundle("~/bundles/DashCSS").Include(
                        "~/assets/plugins/bootstrap/css/bootstrap.min.css",
                        "~/assets/plugins/bootstrap/css/bootstrap-select.css",
                        "~/assets/plugins/select2/select2.css",
                        "~/assets/plugins/tables/jquery.dataTables.min.css",
                        "~/assets/css/all.min.css",
                        "~/assets/css/style.css",
                        "~/assets/css/responsive.css"
            ));



            // BundleTable.EnableOptimizations = true;
        }
    }
}