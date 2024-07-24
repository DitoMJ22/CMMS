using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CMMS.Models
{
    public class Lab
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        UPT _upt = new UPT();

        public Boolean isUniqueID(string id) // ini buat ngecheck no_asset nya udah unik apa belom , kalau tidak ditemukan return false dan sebaliknya
        {
            SqlCommand cmd = new SqlCommand("Select * from Lab where id like @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows) { dr.Close(); con.Close(); return false; } else { dr.Close(); con.Close(); return true; }
            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }
        }

        public Boolean isData(string id) // ini buat ngecheck datanya aktif atau belom kena delete
        {
            SqlCommand cmd = new SqlCommand("Select * from Lab where id like @id and status = @status", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@status", 1);
            con.Open();
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows) { dr.Close(); con.Close(); return true; } else { dr.Close(); con.Close(); return false; }
            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }
        }

        public List<LabModel> getAllData() // ini buat ngambil semua data user
        {
            List<LabModel> labs = new List<LabModel>();
            SqlCommand cmd = new SqlCommand("Select * from Lab where status = @status", con);
            cmd.Parameters.AddWithValue("@status", 1);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                UPTModel upt = _upt.getData(Convert.ToInt32(dr["upt"]));

                labs.Add(new LabModel()
                {
                    id = dr["id"].ToString(),
                    name = dr["name"].ToString(),
                    upt = Convert.ToInt32(dr["upt"]),
                    uptname = upt.name, // ini data uptnya yang udah diambil
                    pic = dr["pic"].ToString(),
                    status = dr["status"].ToString(),
                });
            };
            dr.Close();
            con.Close();
            return labs;
        }

        public LabModel getData(string id)
        {
            LabModel lab = new LabModel();
            SqlCommand cmd = new SqlCommand("Select * from Lab where id like @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            UPTModel upt = _upt.getData(Convert.ToInt32(dr["upt"]));
            lab.id = dr["id"].ToString();
            lab.name = dr["name"].ToString();
            lab.upt = Convert.ToInt32(dr["upt"]);
            lab.uptname = upt.name; // ini data uptnya yang udah diambil
            lab.pic = dr["pic"].ToString();
            lab.status = dr["status"].ToString();
            dr.Close();
            con.Close();
            return lab;
        }

        //insert
        public Boolean insert(LabModel labModel) // ini buat insert data user
        {
            try
            {
                SqlCommand cmd = new SqlCommand("splabinsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", labModel.name);
                cmd.Parameters.AddWithValue("@upt", labModel.upt);
                cmd.Parameters.AddWithValue("@pic", labModel.pic);
                cmd.Parameters.AddWithValue("@status", 1);
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


        //update
        public Boolean update(LabModel LabModel) // ini buat insert data user
        {
            try
            {
                SqlCommand cmd = new SqlCommand("splabupdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", LabModel.id);
                cmd.Parameters.AddWithValue("@name", LabModel.name);
                cmd.Parameters.AddWithValue("@upt", LabModel.upt);
                cmd.Parameters.AddWithValue("@pic", LabModel.pic);
                cmd.Parameters.AddWithValue("@status", 1);
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

        //delete
        public Boolean delete(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("splabdelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@status", 0);
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