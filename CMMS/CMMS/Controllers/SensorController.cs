using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;

namespace CMMS.Controllers
{
    public class SensorController : Controller
    {
        // GET: Sensor
        public string Sensor(string temperature, string id_machine, string nama_sensor, string batas_maintenance)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                try
                {
                    con.Open();
                    using (var cmd = new SqlCommand("sp_insertsensor", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@temperature", temperature);
                        cmd.Parameters.AddWithValue("@id_machine", id_machine);

                        cmd.Parameters.AddWithValue("@nama_sensor", nama_sensor);
                        cmd.Parameters.AddWithValue("@waktu", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@batas_maintenance", batas_maintenance);
                        cmd.ExecuteNonQuery();
                    }

                    //if (Convert.ToInt32(temperature) > Convert.ToInt32(batas_maintenance))
                    //{
                    //    SqlCommand cmd1 = new SqlCommand("SELECT * FROM [User] WHERE role='PIC Maintenance UPT'", con);
                    //    SqlDataReader dr = cmd1.ExecuteReader();
                    //    dr.Read();
                    //    string usermail = dr["email"].ToString();
                    //    dr.Close();
                    //    con.Close();

                    //    string subject = "Early Warning System";
                    //    string body = "Mohon segera melakukan corrective maintenance kepada mesin karena sensor mendeteksi adanya kerusakan.";

                    //    WebMail.Send(usermail, subject, body, null, null, null, true, null, null, null, null, null, null);

                    //}
                    return $"Insert Data to database Successful.";
                }
                catch (Exception ex)
                {
                    return $"Insert Data to database Failed! Exception: {ex.Message}";
                }
            }
        }
    }
}