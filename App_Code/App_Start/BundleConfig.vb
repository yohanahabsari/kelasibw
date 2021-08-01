Imports Microsoft.VisualBasic
Imports System.Web.Optimization

Public Class BundleConfig
    Public Shared Sub RegisterBundles(bundles As BundleCollection)
        Dim jquery As Bundle = New ScriptBundle("~/bundle/jquery").Include("~/bower_components/jquery/dist/jquery.js")
        Dim jqueryui As Bundle = New ScriptBundle("~/bundle/jqueryui").Include("~/bower_components/jquery/dist/jquery.js", "~/Scripts/jquery-ui-{version}.js")
        Dim bootstrap As Bundle = New ScriptBundle("~/bundle/bootstrap").Include("~/bower_components/jquery/dist/jquery.js", "~/Scripts/jquery-ui-{version}.js", "~/bower_components/tether/dist/js/tether.min.js", "~/bower_components/bootstrap/dist/js/bootstrap.js")
        Dim umum As Bundle = New ScriptBundle("~/bundle/umum").Include("~/scripts/jquery-{version}.js", "~/Scripts/jquery-ui-{version}.js", "~/bower_components/tether/dist/js/tether.min.js", "~/bower_components/bootstrap/dist/js/bootstrap.js", "~/js/medium.js", "~/Scripts/DataTables/jquery.dataTables.js", "~/js/addons/datatables.min.js", "~/js/keepalive.js")
        Dim mdb As Bundle = New ScriptBundle("~/bundle/mdb").Include("~/Scripts/mdb/js/mdb.js", "~/js/joane.js")
        Dim tinymce As Bundle = New ScriptBundle("~/bundle/tinymce").Include("~/scripts/jquery-{version}.js", "~/Scripts/jquery-ui-{version}.js", "~/bower_components/tether/dist/js/tether.min.js", "~/bower_components/bootstrap/dist/js/bootstrap.js", "~/js/medium.js", "~/Scripts/DataTables/jquery.dataTables.js", "~/js/addons/datatables.min.js", "~/scripts/tinymce/tinymce.js", "~/js/keepalive.js")
        bundles.Add(jquery)
        bundles.Add(jqueryui)
        bundles.Add(bootstrap)
        bundles.Add(umum)
        bundles.Add(mdb)
        bundles.Add(tinymce)
        ' ></script>
        '<script src="../bower_components/tether/dist/js/tether.min.js"></script>
        '<script src="../bower_components/bootstrap/dist/js/bootstrap.js"></script>
        '<script src="../Scripts/jquery-ui-1.12.1.min.js"></script>")

    End Sub
End Class
