using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CMMS.Models
{
    public class User
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        UPT _upt = new UPT();


        // Login Needs
        public UserModel getUser(string nik, string password) // ini buat ngambil data usernya
        {
            UserModel user = new UserModel();
            SqlCommand cmd = new SqlCommand("Select * from [User] where nik like @nik and password like @password", con);
            cmd.Parameters.AddWithValue("@nik", nik);
            cmd.Parameters.AddWithValue("@password", password);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            try
            {
                user.nik = dr["nik"].ToString();
                user.name = dr["name"].ToString();
                user.password = dr["password"].ToString();
                user.phone = dr["phone"].ToString();
                user.email = dr["email"].ToString();
                user.role = dr["role"].ToString();
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                dr.Close();
                con.Close();
            }
            return user;
        }

        public Boolean isUserValid(string nik, string password) // ini buat ngecheck data user yang login valid apa enggak
        {
            SqlCommand cmd = new SqlCommand("Select * from [User] where nik like @nik and password like @password and status = @status", con);
            cmd.Parameters.AddWithValue("@nik", nik);
            cmd.Parameters.AddWithValue("@password", password);
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


        // CRUD Needs

        public Boolean isUniqueNIK(string nik) // ini buat ngecheck NIKnya udah unik apa belom , kalau tidak ditemukan return false dan sebaliknya
        {
            SqlCommand cmd = new SqlCommand("Select * from [User] where nik like @nik", con);
            cmd.Parameters.AddWithValue("@nik", nik);
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

        public Boolean isData(string nik) // ini buat ngecheck datanya aktif atau belom kena delete
        {
            SqlCommand cmd = new SqlCommand("Select * from [User] where nik like @nik and status = @status", con);
            cmd.Parameters.AddWithValue("@nik", nik);
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

        public List<UserModel> getAllData() // ini buat ngambil semua data user
        {
            List<UserModel> users = new List<UserModel>();
            SqlCommand cmd = new SqlCommand("Select * from [User] where status = @status", con);
            cmd.Parameters.AddWithValue("@status", 1);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                UPTModel upt = _upt.getData(Convert.ToInt32(dr["upt"])); // ini buat ngambil nama upt dari user tertentu

                users.Add(new UserModel()
                {
                    nik = dr["nik"].ToString(),
                    name = dr["name"].ToString(),
                    password = dr["password"].ToString(),
                    phone = dr["phone"].ToString(),
                    email = dr["email"].ToString(),
                    upt = Convert.ToInt32(dr["upt"]),
                    uptname = upt.name, // ini data uptnya yang udah diambil
                    role = dr["role"].ToString(),
                });
            };
            dr.Close();
            con.Close();
            return users;
        }

        public List<UserModel> getPICMaintenance() // ini buat ngambil semua data user
        {
            List<UserModel> users = new List<UserModel>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [User] WHERE (status = 1 AND role = 'PIC Maintenance UPT')", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                UPTModel upt = _upt.getData(Convert.ToInt32(dr["upt"])); // ini buat ngambil nama upt dari user tertentu

                users.Add(new UserModel()
                {
                    nik = dr["nik"].ToString(),
                    name = dr["name"].ToString(),
                    password = dr["password"].ToString(),
                    phone = dr["phone"].ToString(),
                    email = dr["email"].ToString(),
                    upt = Convert.ToInt32(dr["upt"]),
                    uptname = upt.name, // ini data uptnya yang udah diambil
                    role = dr["role"].ToString(),
                });
            };
            dr.Close();
            con.Close();
            return users;
        }

        public List<UserModel> getPICMaintenance1() // ini buat ngambil semua data user
        {
            List<UserModel> users = new List<UserModel>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [User] WHERE status = 1 AND (role = 'Maintenance' OR role = 'Kepala Maintenance')", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                UPTModel upt = _upt.getData(Convert.ToInt32(dr["upt"])); // ini buat ngambil nama upt dari user tertentu

                users.Add(new UserModel()
                {
                    nik = dr["nik"].ToString(),
                    name = dr["name"].ToString(),
                    password = dr["password"].ToString(),
                    phone = dr["phone"].ToString(),
                    email = dr["email"].ToString(),
                    upt = Convert.ToInt32(dr["upt"]),
                    uptname = upt.name, // ini data uptnya yang udah diambil
                    role = dr["role"].ToString(),
                });
            };
            dr.Close();
            con.Close();
            return users;
        }


        public UserModel getData(string id)
        {
            UserModel user = new UserModel();
            SqlCommand cmd = new SqlCommand("Select * from [User] where nik like @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            if (dr.HasRows)
            {
                UPTModel upt = _upt.getData(user.upt);
                user.nik = dr["nik"].ToString();
                user.name = dr["name"].ToString();
                user.password = dr["password"].ToString();
                user.phone = dr["phone"].ToString();
                user.email = dr["email"].ToString();
                user.upt = Convert.ToInt32(dr["upt"]);
                user.uptname = upt.name; // ini data uptnya yang udah diambil
                user.role = dr["role"].ToString();
            }
            else
            {

            }
            dr.Close();
            con.Close();
            return user;
        }




        //insert
        public Boolean insert(UserModel userModel) // ini buat insert data user
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spuserinsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nik", userModel.nik);
                cmd.Parameters.AddWithValue("@name", userModel.name);
                cmd.Parameters.AddWithValue("@password", userModel.password);
                cmd.Parameters.AddWithValue("@upt", userModel.upt);
                cmd.Parameters.AddWithValue("@phone", userModel.phone);
                cmd.Parameters.AddWithValue("@email", userModel.email);
                cmd.Parameters.AddWithValue("@role", userModel.role);
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
        public Boolean update(UserModel userModel) // ini buat insert data user
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spuserupdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nik", userModel.nik);
                cmd.Parameters.AddWithValue("@name", userModel.name);
                cmd.Parameters.AddWithValue("@password", userModel.password);
                cmd.Parameters.AddWithValue("@upt", userModel.upt);
                cmd.Parameters.AddWithValue("@phone", userModel.phone);
                cmd.Parameters.AddWithValue("@email", userModel.email);
                cmd.Parameters.AddWithValue("@role", userModel.role);
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
                SqlCommand cmd = new SqlCommand("spuserdelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nik", id);
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

        public List<UserModel> getAllMaintenance()
        {
            List<UserModel> userModels = new List<UserModel>();

            SqlCommand cmd = new SqlCommand("Select * from [User] where status = @status and role = @role", con);
            cmd.Parameters.AddWithValue("@status", 1);
            cmd.Parameters.AddWithValue("@role", "Maintenance");
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                UPTModel upt = _upt.getData(Convert.ToInt32(dr["upt"])); // ini buat ngambil nama upt dari user tertentu

                userModels.Add(new UserModel()
                {
                    nik = dr["nik"].ToString(),
                    name = dr["name"].ToString(),
                    password = dr["password"].ToString(),
                    phone = dr["phone"].ToString(),
                    email = dr["email"].ToString(),
                    upt = Convert.ToInt32(dr["upt"]),
                    uptname = upt.name, // ini data uptnya yang udah diambil
                    role = dr["role"].ToString(),
                });
            };
            dr.Close();
            con.Close();

            return userModels;
        }
    }
}