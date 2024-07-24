using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CMMS.Models
{
    public class Callendar
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public string idcallendar() //Get last ID Callendar
        {
            string id = "";
            SqlCommand cmd = new SqlCommand("SELECT (IDENT_CURRENT('Callendar') + 1) AS lastid", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            if (dr["lastid"] == DBNull.Value)
            {
                id = "1";
            }
            else
            {
                id = dr["lastid"].ToString();
            }

            dr.Close();
            con.Close();
            return id;
        }

        public Boolean insert(CallendarModel callendarModel) // ini buat insert data callendar
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_insertcallendar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@title", callendarModel.title);
                cmd.Parameters.AddWithValue("@description", callendarModel.description);
                cmd.Parameters.AddWithValue("@start", Convert.ToDateTime(callendarModel.start));
                cmd.Parameters.AddWithValue("@end", Convert.ToDateTime(callendarModel.end));
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }
        }

        public Boolean update(CallendarModel callendarModel) // ini buat insert data user
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_updatecallendar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", callendarModel.id);
                cmd.Parameters.AddWithValue("@title", callendarModel.title);
                cmd.Parameters.AddWithValue("@description", callendarModel.description);
                cmd.Parameters.AddWithValue("@start", Convert.ToDateTime(callendarModel.start));
                cmd.Parameters.AddWithValue("@end", Convert.ToDateTime(callendarModel.end));
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }
        }

        public Boolean delete(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_deletecallendar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }
        }

        public Boolean finish(int id) // ini buat insert data user
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_finishcallendar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@end", DateTime.Now);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }
        }
    }
}