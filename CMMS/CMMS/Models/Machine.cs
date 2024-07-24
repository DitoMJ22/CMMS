using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CMMS.Models
{
    public class Machine
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        // CRUD Needs
        public string idphoto() //Get last ID Photo
        {
            string id = "";
            SqlCommand cmd = new SqlCommand("SELECT (IDENT_CURRENT('Detail_PhotoMachine') + 1) AS lastid", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            if (dr.HasRows)
            {
                id = dr["lastid"].ToString();
            }
            else
            {
                id = "1";
            }

            dr.Close();
            con.Close();
            return id;
        }

        public List<string> getAll_photoMachine(string id_machine) //Get All Photo
        {
            List<string> photos = new List<string>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Detail_PhotoMachine WHERE id_machine=@id_machine", con);
            cmd.Parameters.AddWithValue("@id_machine", id_machine);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                photos.Add(dr["photo"].ToString());
            }

            dr.Close();
            con.Close();
            return photos;
        }

        public Boolean delete_photoMachine(string id) //Hapus foto mesin dari detail mesin
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spphotomachinedelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_photo", id);
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

        public Boolean isUniqueNoAsset(string no_asset) // ini buat ngecheck no_asset nya udah unik apa belom , kalau tidak ditemukan return false dan sebaliknya
        {
            SqlCommand cmd = new SqlCommand("Select * from Machine where no_asset like @no_asset", con);
            cmd.Parameters.AddWithValue("@no_asset", no_asset);
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
            SqlCommand cmd = new SqlCommand("Select * from Machine where no_asset like @no_asset and status = @status", con);
            cmd.Parameters.AddWithValue("@no_asset", id);
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

        public List<MachineModel> getAllData() // ini buat ngambil semua data user
        {
            List<MachineModel> machines = new List<MachineModel>();
            SqlCommand cmd = new SqlCommand("Select * from Machine where status = @status", con);
            cmd.Parameters.AddWithValue("@status", 1);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                machines.Add(new MachineModel()
                {
                    no_asset = dr["no_asset"].ToString(),
                    name = dr["name"].ToString(),
                    model = dr["model"].ToString(),
                    merk = dr["merk"].ToString(),
                    year = dr["year"].ToString(),
                    condition = dr["condition"].ToString(),
                    lab = dr["lab"].ToString(),
                    status = dr["status"].ToString(),
                });
            };
            dr.Close();
            con.Close();
            return machines;
        }

        public MachineModel getData(string id)
        {
            MachineModel machine = new MachineModel();
            SqlCommand cmd = new SqlCommand("SELECT no_asset, m.name, model, merk, year, condition, b.name AS lab, m.status FROM Machine AS m JOIN lab AS b ON m.lab = b.id WHERE no_asset like @no_asset", con);
            cmd.Parameters.AddWithValue("@no_asset", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            machine.no_asset = dr["no_asset"].ToString();
            machine.name = dr["name"].ToString();
            machine.model = dr["model"].ToString();
            machine.merk = dr["merk"].ToString();
            machine.year = dr["year"].ToString();
            machine.condition = dr["condition"].ToString();
            machine.lab = dr["lab"].ToString();
            machine.status = dr["status"].ToString();
            dr.Close();
            con.Close();
            return machine;
        }

        public MachineModel getData2(string id)
        {
            MachineModel machine = new MachineModel();
            SqlCommand cmd = new SqlCommand("SELECT * FROM machine", con);
            cmd.Parameters.AddWithValue("@no_asset", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            machine.no_asset = dr["no_asset"].ToString();
            machine.name = dr["name"].ToString();
            machine.model = dr["model"].ToString();
            machine.merk = dr["merk"].ToString();
            machine.year = dr["year"].ToString();
            machine.condition = dr["condition"].ToString();
            machine.lab = dr["lab"].ToString();
            machine.status = dr["status"].ToString();
            dr.Close();
            con.Close();
            return machine;
        }
        //insert
        public Boolean insert(MachineModel machineModel) // ini buat insert data user
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spmachineinsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@no_asset", machineModel.no_asset);
                cmd.Parameters.AddWithValue("@name", machineModel.name);
                cmd.Parameters.AddWithValue("@model", machineModel.model);
                cmd.Parameters.AddWithValue("@merk", machineModel.merk);
                cmd.Parameters.AddWithValue("@year", machineModel.year);
                cmd.Parameters.AddWithValue("@condition", machineModel.condition);
                cmd.Parameters.AddWithValue("@lab", machineModel.lab);
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

        //insert photo
        public Boolean insert_photo(string no_asset, string photo_name) // ini buat insert photo
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spphotomachineinsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_machine", no_asset);
                cmd.Parameters.AddWithValue("@photo", photo_name);

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
        public Boolean update(MachineModel machineModel) // ini buat insert data user
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spmachineupdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@no_asset", machineModel.no_asset);
                cmd.Parameters.AddWithValue("@name", machineModel.name);
                cmd.Parameters.AddWithValue("@model", machineModel.model);
                cmd.Parameters.AddWithValue("@merk", machineModel.merk);
                cmd.Parameters.AddWithValue("@year", machineModel.year);
                cmd.Parameters.AddWithValue("@condition", machineModel.condition);
                cmd.Parameters.AddWithValue("@lab", machineModel.lab);
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
                SqlCommand cmd = new SqlCommand("spmachinedelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@no_asset", id);
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

        //===========================================================================================================================================================//
        //insert photo
        public Boolean insert_sparepartmachine(string no_asset, string photo_name) // ini buat insert photo
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spphotomachineinsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_machine", no_asset);
                cmd.Parameters.AddWithValue("@photo", photo_name);

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