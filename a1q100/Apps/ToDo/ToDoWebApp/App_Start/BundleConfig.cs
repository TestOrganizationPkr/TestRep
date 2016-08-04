using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ToDoWebApp
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            if (null != bundles)
            {
                // MVC in-built client side validation
                bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                            "~/Scripts/jquery.unobtrusive*",
                            "~/Scripts/jquery.validate*"));
            }
        }
    }
}