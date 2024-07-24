using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CMMS.Models
{
    public class MaintenanceCorrective
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public List<MaintenanceCorrectiveModel> getAllWOC() // ini buat ngambil semua data sparepart pada mesin
        {
            List<MaintenanceCorrectiveModel> woc = new List<MaintenanceCorrectiveModel>();
            SqlCommand cmd = new SqlCommand("select * from WO_Corrective order by id_woc desc", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                woc.Add(new MaintenanceCorrectiveModel()
                {
                    id_woc = dr["id_woc"].ToString(),
                    id_machine = dr["id_machine"].ToString(),
                    id_callendar = Convert.ToInt32(dr["id_callendar"]),
                    requested_by = dr["requested_by"].ToString(),
                    maintenance_by = dr["maintenance_by"].ToString(),
                    request_date = Convert.ToDateTime(dr["request_date"]).ToString("dd-MM-yyyy"),
                    deadline = Convert.ToDateTime(dr["deadline"]).ToString("dd-MM-yyyy"),
                    finish_date = Convert.ToDateTime(dr["finish_date"]).ToString("dd-MM-yyyy"),
                    maintenance_cost = Convert.ToInt32(dr["maintenance_cost"]),
                    desc_request = dr["desc_request"].ToString(),
                    desc_maintenance = dr["desc_maintenance"].ToString(),
                    status = dr["status"].ToString(),
                });
            };
            dr.Close();
            con.Close();
            return woc;
        }

        public List<MaintenanceCorrectiveModel> getAllWOCFinished() // ini buat ngambil semua data sparepart pada mesin
        {
            List<MaintenanceCorrectiveModel> woc = new List<MaintenanceCorrectiveModel>();
            SqlCommand cmd = new SqlCommand("select * from WO_Corrective where status = 'Finished'", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                woc.Add(new MaintenanceCorrectiveModel()
                {
                    id_woc = dr["id_woc"].ToString(),
                    id_machine = dr["id_machine"].ToString(),
                    id_callendar = Convert.ToInt32(dr["id_callendar"]),
                    requested_by = dr["requested_by"].ToString(),
                    maintenance_by = dr["maintenance_by"].ToString(),
                    request_date = Convert.ToDateTime(dr["request_date"]).ToString("dd-MM-yyyy"),
                    deadline = Convert.ToDateTime(dr["deadline"]).ToString("dd-MM-yyyy"),
                    finish_date = Convert.ToDateTime(dr["finish_date"]).ToString("dd-MM-yyyy"),
                    maintenance_cost = Convert.ToInt32(dr["maintenance_cost"]),
                    desc_request = dr["desc_request"].ToString(),
                    desc_maintenance = dr["desc_maintenance"].ToString(),
                    status = dr["status"].ToString(),
                });
            };
            dr.Close();
            con.Close();
            return woc;
        }

        public List<MaintenanceCorrectiveModel> getAllWOCMachine(string id_machine) // ini buat ngambil semua data sparepart pada mesin
        {
            List<MaintenanceCorrectiveModel> woc = new List<MaintenanceCorrectiveModel>();
            SqlCommand cmd = new SqlCommand("select * from WO_Corrective where id_machine = @id_machine and status <> @status order by id_woc desc", con);
            cmd.Parameters.AddWithValue("@id_machine", id_machine);
            cmd.Parameters.AddWithValue("@status", "Finished");
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                woc.Add(new MaintenanceCorrectiveModel()
                {
                    id_woc = dr["id_woc"].ToString(),
                    id_machine = dr["id_machine"].ToString(),
                    id_callendar = Convert.ToInt32(dr["id_callendar"]),
                    requested_by = dr["requested_by"].ToString(),
                    maintenance_by = dr["maintenance_by"].ToString(),
                    request_date = Convert.ToDateTime(dr["request_date"]).ToString("dd-MM-yyyy"),
                    deadline = Convert.ToDateTime(dr["deadline"]).ToString("dd-MM-yyyy"),
                    finish_date = Convert.ToDateTime(dr["finish_date"]).ToString("dd-MM-yyyy"),
                    maintenance_cost = Convert.ToInt32(dr["maintenance_cost"]),
                    sparepart_cost = Convert.ToInt32(dr["sparepart_cost"]),
                    cost = Convert.ToInt32(dr["cost"]),
                    desc_request = dr["desc_request"].ToString(),
                    desc_maintenance = dr["desc_maintenance"].ToString(),
                    status = dr["status"].ToString(),
                });
            };
            dr.Close();
            con.Close();
            return woc;
        }

        public MaintenanceCorrectiveModel getData(string id)
        {
            MaintenanceCorrectiveModel woc = new MaintenanceCorrectiveModel();
            SqlCommand cmd = new SqlCommand("select * from WO_Corrective where id_woc = @id_woc order by id_woc desc", con);
            cmd.Parameters.AddWithValue("@id_woc", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            woc.id_woc = dr["id_woc"].ToString();
            woc.id_machine = dr["id_machine"].ToString();
            woc.id_callendar = Convert.ToInt32(dr["id_callendar"]);
            woc.requested_by = dr["requested_by"].ToString();
            woc.maintenance_by = dr["maintenance_by"].ToString();
            woc.request_date = Convert.ToDateTime(dr["request_date"]).ToString("dd-MM-yyyy");
            woc.deadline = Convert.ToDateTime(dr["deadline"]).ToString("dd-MM-yyyy");
            woc.finish_date = Convert.ToDateTime(dr["finish_date"]).ToString("dd-MM-yyyy");
            woc.maintenance_cost = Convert.ToInt32(dr["maintenance_cost"]);
            woc.sparepart_cost = Convert.ToInt32(dr["sparepart_cost"]);
            woc.cost = Convert.ToInt32(dr["cost"]);
            woc.desc_request = dr["desc_request"].ToString();
            woc.desc_maintenance = dr["desc_maintenance"].ToString();
            woc.status = dr["status"].ToString();
            dr.Close();
            con.Close();
            return woc;
        }

        public List<MaintenanceCorrectiveModel> getMyWOC(string id_user) // ini buat ngambil semua data sparepart pada mesin
        {
            List<MaintenanceCorrectiveModel> woc = new List<MaintenanceCorrectiveModel>();
            SqlCommand cmd = new SqlCommand("select * from WO_Corrective where (status = @status OR status = @status2) and maintenance_by = @maintenance_by order by id_woc desc", con);
            cmd.Parameters.AddWithValue("@status", "Maintenance");
            cmd.Parameters.AddWithValue("@status2", "Remaintenance");
            cmd.Parameters.AddWithValue("@maintenance_by", id_user);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                woc.Add(new MaintenanceCorrectiveModel()
                {
                    id_woc = dr["id_woc"].ToString(),
                    id_machine = dr["id_machine"].ToString(),
                    id_callendar = Convert.ToInt32(dr["id_callendar"]),
                    requested_by = dr["requested_by"].ToString(),
                    maintenance_by = dr["maintenance_by"].ToString(),
                    request_date = Convert.ToDateTime(dr["request_date"]).ToString("dd-MM-yyyy"),
                    deadline = Convert.ToDateTime(dr["deadline"]).ToString("dd-MM-yyyy"),
                    finish_date = Convert.ToDateTime(dr["finish_date"]).ToString("dd-MM-yyyy"),
                    maintenance_cost = Convert.ToInt32(dr["maintenance_cost"]),
                    desc_request = dr["desc_request"].ToString(),
                    desc_maintenance = dr["desc_maintenance"].ToString(),
                    status = dr["status"].ToString(),
                });
            };
            dr.Close();
            con.Close();
            return woc;
        }

        //==============================================================================================================================================//
        public string getWorkOrderID(string key)
        {
            string id = key.Substring(0, key.IndexOf("/"));

            return id;
        }

        public string setWorkOrderCorrectiveID(string key)
        {
            SqlCommand cmd = new SqlCommand("select * from(select left(id_woc, charindex('/', id_woc)-1) as [key],id_woc FROM WO_Corrective) a where [key] = @key", con);
            cmd.Parameters.AddWithValue("@key", key);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            string id = dr["id_woc"].ToString();
            dr.Close();
            con.Close();
            return id;
        }


        public Boolean maintenanceupdate(MaintenanceCorrectiveModel maintenanceCorrectiveModel) // ini buat insert data user
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_updatemaintenancewoc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_woc", maintenanceCorrectiveModel.id_woc);
                cmd.Parameters.AddWithValue("@maintenance_cost", maintenanceCorrectiveModel.maintenance_cost);
                cmd.Parameters.AddWithValue("@desc_maintenance", maintenanceCorrectiveModel.desc_maintenance);
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

        public Boolean maintenancefinish(string id) // ini buat insert data user
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_finishmaintenancewoc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_woc", id);
                cmd.Parameters.AddWithValue("@finish_date", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@status", "Checking");
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

        public Boolean insert_sparepartwoc(string id_woc, string quantity, int cost, string id_sparepart) // ini buat insert sparepart machine
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_insertsparepartwoc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_woc", id_woc);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@cost", cost);
                cmd.Parameters.AddWithValue("@id_sparepart", id_sparepart);
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

        public Boolean delete_sparepartmachinewoc(string id_woc, string id_sparepart) //Hapus foto mesin dari detail mesin
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_deletesparepartwoc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_woc", id_woc);
                cmd.Parameters.AddWithValue("@id_sparepart", id_sparepart);
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

        public List<string> getAll_photoCorrective(string id_woc) //Get All Photo
        {
            List<string> photos = new List<string>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM WOC_Photo WHERE id_woc=@id_woc", con);
            cmd.Parameters.AddWithValue("@id_woc", id_woc);
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

        public Boolean delete_photoCorrective(string id) //Hapus foto part dari detail part
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_deletephotocorrective", con);
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

        public Boolean insert_photo(string id, string photo_name) // ini buat insert photo
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_insertphotocorrective", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_woc", id);
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

        public string idphoto() //Get last ID Photo
        {
            string id = "";
            SqlCommand cmd = new SqlCommand("SELECT (IDENT_CURRENT('WOC_Photo') + 1) AS lastid", con);
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
    }
}