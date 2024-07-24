using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CMMS.Models
{
    public class Report
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public string counttotaluser() //Get last ID Sparepart
        {
            string key = "";
            SqlCommand cmd = new SqlCommand("select count(nik) as [key] from [User] where status = 1", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            key = dr["key"].ToString();

            dr.Close();
            con.Close();
            return key;
        }

        public string counttotalmachine()
        {
            string key = "";
            SqlCommand cmd = new SqlCommand("select count(no_asset) as [key] from Machine where status = 1", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            key = dr["key"].ToString();

            dr.Close();
            con.Close();
            return key;
        }

        public string counttotalsparepart()
        {
            string key = "";
            SqlCommand cmd = new SqlCommand("select count(id) as [key] from Sparepart where status = 1", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            key = dr["key"].ToString();

            dr.Close();
            con.Close();
            return key;
        }

        public string counttotallab()
        {
            string key = "";
            SqlCommand cmd = new SqlCommand("select count(id) as [key] from Lab where status = 1", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            key = dr["key"].ToString();

            dr.Close();
            con.Close();
            return key;
        }

        public string counttotalwocuser(string id)
        {
            string key = "";
            SqlCommand cmd = new SqlCommand("select count(id_woc) as [key] from WO_Corrective where requested_by = @requested_by", con);
            cmd.Parameters.AddWithValue("@requested_by", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            key = dr["key"].ToString();

            dr.Close();
            con.Close();
            return key;
        }

        public string countTotalWOMaintenance(string nik)
        {
            string key = "";
            SqlCommand cmd = new SqlCommand("BEGIN SELECT(SELECT COUNT(*) FROM WO_Corrective WHERE [status] = 'Maintenance' AND maintenance_by = @nik) +" +
                "(SELECT COUNT(*) FROM WO_Preventive WHERE [status] = 'Maintenance' AND maintenance_by = @nik) AS TotalData END", con);
            cmd.Parameters.AddWithValue("@nik", nik);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            key = dr["TotalData"].ToString();

            dr.Close();
            con.Close();
            return key;
        }

        public string counttotalwocsubmitted()
        {
            string key = "";
            SqlCommand cmd = new SqlCommand("select count(id_woc) as [key] from WO_Corrective where status='Submitted'", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            key = dr["key"].ToString();

            dr.Close();
            con.Close();
            return key;
        }

        public string counttotalwopsubmitted()
        {
            string key = "";
            SqlCommand cmd = new SqlCommand("select count(id_wop) as [key] from WO_Preventive where status='Submitted'", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            key = dr["key"].ToString();

            dr.Close();
            con.Close();
            return key;
        }


        public string counttotalwopuser(string id)
        {
            string key = "";
            SqlCommand cmd = new SqlCommand("select count(id_wop) as [key] from WO_Preventive where requested_by = @requested_by", con);
            cmd.Parameters.AddWithValue("@requested_by", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            key = dr["key"].ToString();

            dr.Close();
            con.Close();
            return key;
        }


        public string counttotalWO_Plot() //Get last ID Sparepart
        {
            string key = "";
            SqlCommand cmd = new SqlCommand("SELECT(SELECT COUNT(*) FROM WO_Corrective WHERE status = 'Approved') + " +
                "(SELECT COUNT(*) FROM WO_Preventive WHERE status = 'Approved') AS TotalData", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            key = dr["TotalData"].ToString();

            dr.Close();
            con.Close();
            return key;
        }

        public string counttotalWO_Maintenance() //Get last ID Sparepart
        {
            string key = "";
            SqlCommand cmd = new SqlCommand("SELECT (SELECT COUNT(*) FROM WO_Corrective WHERE status = 'Maintenance' AND maintenance_by = '9663') +" +
                " (SELECT COUNT(*) FROM WO_Preventive WHERE status = 'Maintenance' AND maintenance_by = '9663') AS TotalData", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            key = dr["TotalData"].ToString();

            dr.Close();
            con.Close();
            return key;
        }

        public string counttotalWO_Checking() //Get last ID Sparepart
        {
            string key = "";
            SqlCommand cmd = new SqlCommand("SELECT (SELECT COUNT(*) FROM WO_Corrective WHERE status = 'Checking') +" +
                " (SELECT COUNT(*) FROM WO_Preventive WHERE status = 'Checking') AS TotalData", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            key = dr["TotalData"].ToString();

            dr.Close();
            con.Close();
            return key;
        }

        public string countAllWO()
        {
            string key = "";
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) as total_finished FROM(SELECT status FROM WO_Corrective WHERE status = 'finished'" +
                "UNION ALL " +
                "SELECT status FROM WO_Preventive WHERE status = 'finished') as combined_table", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            key = dr["total_finished"].ToString();

            dr.Close();
            con.Close();
            return key;
        }

        public string countAllWOSort(string endDate, string startDate)
        {
            string key = "";
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) as total_finished FROM(SELECT status FROM WO_Corrective WHERE status = 'finished' AND finish_date BETWEEN '" + startDate + "' AND '" + endDate + "' UNION ALL SELECT status FROM WO_Preventive " +
                            "WHERE status = 'finished' AND finish_date BETWEEN '" + startDate + "' AND '" + endDate + "') as combined_table", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            key = dr["total_finished"].ToString();

            dr.Close();
            con.Close();
            return key;
        }

        public int countAllWO_Cost()
        {
            int key;
            SqlCommand cmd = new SqlCommand("SELECT SUM(COALESCE(WO_Corrective.Cost, 0) + COALESCE(WO_Preventive.Cost, 0)) as TotalCost FROM WO_Corrective " +
                "FULL OUTER JOIN WO_Preventive ON WO_Corrective.id_woc = WO_Preventive.id_wop " +
                "WHERE WO_Corrective.Status = 'finished' OR WO_Preventive.Status = 'finished'", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            key = Convert.ToInt32(dr["TotalCost"]);

            dr.Close();
            con.Close();
            return key;
        }

        public string countAllWO_CostSort(string endDate, string startDate)
        {
            string key = "";
            SqlCommand cmd = new SqlCommand("SELECT SUM(COALESCE(WO_Corrective.Cost, 0) + COALESCE(WO_Preventive.Cost, 0)) as TotalCost FROM WO_Corrective FULL OUTER " +
                "JOIN WO_Preventive ON WO_Corrective.id_woc = WO_Preventive.id_wop WHERE(WO_Corrective.Status = 'finished' OR WO_Preventive.Status = 'finished') " +
                "AND(WO_Corrective.finish_date BETWEEN '" + startDate + "' AND '" + endDate + "') OR(WO_Preventive.finish_date BETWEEN '" + startDate + "' AND '" + endDate + "')", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            key = Convert.ToString(dr["TotalCost"]);
            key = string.Format(CultureInfo.GetCultureInfo("id-ID"), "{0:n0}", key);
            key = Convert.ToString(key).Replace(",0000", "");
            dr.Close();
            con.Close();
            return key;
        }
    }
}