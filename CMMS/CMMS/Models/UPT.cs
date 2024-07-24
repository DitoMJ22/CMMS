using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CMMS.Models
{
    public class UPT
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        public List<UPTModel> getAllData()
        {
            List<UPTModel> upts = new List<UPTModel>();
            upts.Add(new UPTModel()
            {
                id = null,
                name = "Choose UPT",
            });
            SqlCommand cmd = new SqlCommand("Select * from UPT ", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                upts.Add(new UPTModel()
                {
                    id = dr["id"].ToString(),
                    name = dr["name"].ToString(),
                });
            };
            dr.Close();
            con.Close();
            return upts;
        }

        public UPTModel getData(int id)
        {
            UPTModel upt = new UPTModel();
            SqlCommand cmd = new SqlCommand("Select * from UPT where id = @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {

                upt.id = dr[0].ToString();
                upt.name = dr[1].ToString();
            }
            else
            {

            }

            dr.Close();
            con.Close();
            return upt;
        }
    }
}