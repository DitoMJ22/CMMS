using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CMMS.Models
{
    public class Monitoring
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public List<MonitoringModel> getAll()
        {
            List<MonitoringModel> woMonitoring = new List<MonitoringModel>();

            SqlCommand cmd = new SqlCommand("select * from Sensor", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                woMonitoring.Add(new MonitoringModel()
                {
                    id_monitoring = dr["id_monitoring"].ToString(),
                    temperature = dr["temperature"].ToString(),
                    id_machine = dr["id_machine"].ToString(),
                    nama_sensor = dr["nama_sensor"].ToString(),
                    waktu = Convert.ToDateTime(dr["waktu"]).ToString("dd-MM-yyyy"),

                });
            };
            dr.Close();
            con.Close();


            return woMonitoring;
        }
    }
}