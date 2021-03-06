﻿using System.Web;
using System.Web.Optimization;

namespace SmartShopWebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap.min.js",
                       "~/Scripts/toastr.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/wijmo.min.js",
                      "~/Scripts/wijmo.grid.min.js",
                      "~/Scripts/wijmo.input.min.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Scripts/toastr.css",
                      "~/Content/wijmo.css",
                      "~/Content/wijmo.min.css",
                      "~/Content/site.css",
                      "~/Content/themes/wijmo.theme.midnight.min.css",
                      "~/fonts/css/font-awesome.css",
                      "~/fonts/css/font-awesome.min.css"));
        }
    }
}
