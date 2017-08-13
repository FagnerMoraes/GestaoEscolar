using System;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

namespace GestaoEscolar.Controllers
{
    [Authorize]
    public class BackupController : Controller
    {
        private const string ConnectionString = "server=localhost;user=root;pwd=fmc2312;database=bdgestao;";

        public ActionResult Index()
        {
            string filePhysicalPath = HttpContext.Server.MapPath("~/backup.sql");

            using (var conn = new MySqlConnection(ConnectionString))
            {
                using (var cmd = new MySqlCommand())
                {
                    using (var mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ExportToFile(filePhysicalPath);
                    }
                }
            }

            Response.ContentType = "text/plain";
            Response.AppendHeader("Content-Disposition", "attachment; filename=backup" + DateTime.Now + ".sql");
            Response.TransmitFile(filePhysicalPath);
            Response.End();


            return RedirectToAction("Index");
        }


        public ActionResult Restaurar()
        {

            string filePhysicalPath = HttpContext.Server.MapPath("~/upload.sql");
            
            using (var conn = new MySqlConnection(ConnectionString))
            {
                using (var cmd = new MySqlCommand())
                {
                    using (var mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ImportFromFile(filePhysicalPath);
                    }
                }
            }
            Response.Write("<script type=\"text/javascript\">alert('Import Completed')</script>");



            return RedirectToAction("Index");
        }

    }
}
