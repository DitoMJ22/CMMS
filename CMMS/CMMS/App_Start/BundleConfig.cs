using System.Web;
using System.Web.Optimization;

namespace CMMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/assets/vendors/core/core.css", // css utama
                "~/Content/assets/css/demo_1/style.min.css", // css layout 
                "~/Content/assets/fonts/feather-font/css/iconfont.css", // css base font icon
                "~/Content/assets/vendors/font-awesome/css/font-awesome.min.css", // css font icon
                "~/Content/assets/vendors/datatables.net-bs4/dataTables.bootstrap4.css", // css datatable
                "~/Content/assets/vendors/prismjs/themes/prism.css", // css component
                "~/Content/assets/vendors/select2/select2.min.css", // css input
                "~/Content/assets/vendors/jquery-tags-input/jquery.tagsinput.min.css", // css input
                "~/Content/assets/vendors/dropzone/dropzone.min.css", // css input file
                "~/Content/assets/vendors/dropify/dist/dropify.min.css", // css input file
                "~/Content/assets/vendors/bootstrap-colorpicker/bootstrap-colorpicker.min.css", // css color picker
                "~/Content/assets/vendors/bootstrap-datepicker/bootstrap-datepicker.min.css", // css date picker
                "~/Content/assets/vendors/tempusdominus-bootstrap-4/tempusdominus-bootstrap-4.min.css", // css bootstrap
                "~/Content/assets/vendors/owl.carousel/owl.carousel.min.css", // css carousel
                "~/Content/assets/vendors/owl.carousel/owl.theme.default.min.css", // css carousel
                "~/Content/assets/vendors/animate.css/animate.min.css", // css carousel
                "~/Content/assets/vendors/fullcalendar/fullcalendar.min.css" // css fullcalendar
                ));

            bundles.Add(new Bundle("~/Content/js").Include(
               "~/Content/assets/vendors/core/core.js", // js utama
               "~/Content/assets/js/template.js", // js layout
               "~/Content/assets/vendors/feather-icons/feather.min.js", // js base font icon
               "~/Content/assets/vendors/jquery-validation/jquery.validate.min.js", // js validasi
               "~/Content/assets/vendors/bootstrap-maxlength/bootstrap-maxlength.min.js", // js validasi length
               "~/Content/assets/vendors/inputmask/jquery.inputmask.min.js", // js validasi mask
               "~/Content/assets/vendors/select2/select2.min.js", // js input
               "~/Content/assets/vendors/typeahead.js/typeahead.bundle.min.js", // gatau ini js apa wkwk
               "~/Content/assets/vendors/jquery-tags-input/jquery.tagsinput.min.js", // js input
               "~/Content/assets/vendors/dropzone/dropzone.min.js", // js input file
               "~/Content/assets/vendors/dropify/dist/dropify.min.js", // js input file
               "~/Content/assets/vendors/bootstrap-colorpicker/bootstrap-colorpicker.min.js", // js color picker
               "~/Content/assets/vendors/bootstrap-datepicker/bootstrap-datepicker.min.js", // js date picker
               "~/Content/assets/vendors/moment/moment.min.js", // js moment
               "~/Content/assets/vendors/tempusdominus-bootstrap-4/tempusdominus-bootstrap-4.js", // js bootstrap
               "~/Content/assets/vendors/owl.carousel/owl.carousel.min.js", // js carousel
               "~/Content/assets/vendors/jquery-mousewheel/jquery.mousewheel.js", // js carousel
               "~/Content/assets/vendors/promise-polyfill/polyfill.min.js", // js 
               "~/Content/assets/vendors/chartjs/Chart.min.js", // js chart
               "~/Content/assets/vendors/jquery.flot/jquery.flot.js", // js flot
               "~/Content/assets/vendors/jquery.flot/jquery.flot.resize.js", // js flot
               "~/Content/assets/vendors/apexcharts/apexcharts.min.js", // js chart
               "~/Content/assets/js/chartjs-light.js", // js chart
               "~/Content/assets/js/template.js", // js chart
               "~/Content/assets/vendors/progressbar.js/progressbar.min.js", // js progressbar
               "~/Content/assets/vendors/datatables.net/jquery.dataTables.js",
               "~/Content/assets/vendors/datatables.net-bs4/dataTables.bootstrap4.js",
               "~/Content/assets/js/data-table.js",
               "~/Content/assets/vendors/fullcalendar/fullcalendar.min.js", // js fullcalendar
               "~/Content/assets/js/fullcalendar.js", // js fullcalendar
                "~/Content/assets/vendors/tinymce/tinymce.min.js", // js fullcalendar
                "~/Content/assets/js/tinymce.js" // js fullcalendar

               ));
        }
    }
}
