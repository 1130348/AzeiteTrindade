using System.Web;
using System.Web.Optimization;

namespace LusiadasSolucaoWeb
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            //bundles.IgnoreList.Clear();

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/plugins/jQuery/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/plugins/jQuery/jquery-ui-{version}.js"));

            //bundles.Add(new StyleBundle("~/bundles/syleTeste").Include(
            //            "~/plugins/jQuery/jquery-{version}.js"));


            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/css/web.min.css"));


    //@*<link href="/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    //<link href="/Content/css/font-awesome.min.css" rel="stylesheet" />
    //<link href="/Content/css/ionicons.min.css" rel="stylesheet" />
    //<link href="/dist/css/AdminLTE.min.css" rel="stylesheet" type="text/css" />
    //<link href="/dist/css/skins/_all-skins.min.css" rel="stylesheet" type="text/css" />
    //<link href="/plugins/iCheck/flat/blue.css" rel="stylesheet" type="text/css" />
    //<link href="/plugins/morris/morris.css" rel="stylesheet" type="text/css" />
    //<link href="/plugins/jvectormap/jquery-jvectormap-1.2.2.css" rel="stylesheet" type="text/css" />
    //<link href="/plugins/datepicker/datepicker3.css" rel="stylesheet" type="text/css" />
    //<link href="/plugins/daterangepicker/daterangepicker-bs3.css" rel="stylesheet" type="text/css" />
    //<link href="/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css" rel="stylesheet" type="text/css" />*@
            bundles.Add(new ScriptBundle("~/plugins/datatables/").Include("~/plugins/datatables/*.js"));

            bundles.Add(new StyleBundle("~/bootstrap/css").Include("~/bootstrap/css/bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/plugins/datatables/").Include("~/plugins/datatables/*.css"));

  //<link href="/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
  //  <link href="/Content/css/font-awesome.min.css" rel="stylesheet" />
  //  <link href="/Content/css/ionicons.min.css" rel="stylesheet" />
  //  <link href="/dist/css/AdminLTE.min.css" rel="stylesheet" type="text/css" />
  //  <link href="/dist/css/skins/_all-skins.min.css" rel="stylesheet" type="text/css" />
  //  <link href="/plugins/iCheck/flat/blue.css" rel="stylesheet" type="text/css" />
  //  <link href="/plugins/morris/morris.css" rel="stylesheet" type="text/css" />
  //  <link href="/plugins/jvectormap/jquery-jvectormap-1.2.2.css" rel="stylesheet" type="text/css" />
  //  <link href="/plugins/datepicker/datepicker3.css" rel="stylesheet" type="text/css" />
  //  <link href="/plugins/daterangepicker/daterangepicker-bs3.css" rel="stylesheet" type="text/css" />
  //  <link href="/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css" rel="stylesheet" type="text/css" />

  //  <script src="/plugins/jQuery/jQuery-2.1.4.min.js"></script>
  //  <script src="/plugins/datatables/dataTables.bootstrap.min.js"></script>
  //  <script src="/plugins/datatables/jquery.dataTables.min.js"></script>

  //  <link href="/plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" />
  //  <link href="/plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" />
  //  <link href="/plugins/datatables/jquery.dataTables_themeroller.css" rel="stylesheet" />
        }
    }
}