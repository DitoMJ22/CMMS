using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CMMS.Models
{
    public class MaintenancePreventive
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public List<MaintenancePreventiveModel> getAllWOP() // ini buat ngambil semua data sparepart pada mesin
        {
            List<MaintenancePreventiveModel> wop = new List<MaintenancePreventiveModel>();
            SqlCommand cmd = new SqlCommand("select * from WO_Preventive order by id_wop desc", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                wop.Add(new MaintenancePreventiveModel()
                {
                    id_wop = dr["id_wop"].ToString(),
                    id_machine = dr["id_machine"].ToString(),
                    id_callendar = Convert.ToInt32(dr["id_callendar"]),
                    requested_by = dr["requested_by"].ToString(),
                    maintenance_by = dr["maintenance_by"].ToString(),
                    schedule_date = Convert.ToDateTime(dr["schedule_date"]).ToString("dd-MM-yyyy"),
                    start_date = Convert.ToDateTime(dr["start_date"]).ToString("dd-MM-yyyy"),
                    finish_date = Convert.ToDateTime(dr["finish_date"]).ToString("dd-MM-yyyy"),
                    maintenance_cost = Convert.ToInt32(dr["maintenance_cost"]),
                    description = dr["description"].ToString(),
                    desc_maintenance = dr["desc_maintenance"].ToString(),
                    status = dr["status"].ToString(),
                });
            };
            dr.Close();
            con.Close();
            return wop;
        }

        public List<MaintenancePreventiveModel> getAllWOPFinished() // ini buat ngambil semua data sparepart pada mesin
        {
            List<MaintenancePreventiveModel> wop = new List<MaintenancePreventiveModel>();
            SqlCommand cmd = new SqlCommand("select * from WO_Preventive where status = 'Finished'", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                wop.Add(new MaintenancePreventiveModel()
                {
                    id_wop = dr["id_wop"].ToString(),
                    id_machine = dr["id_machine"].ToString(),
                    id_callendar = Convert.ToInt32(dr["id_callendar"]),
                    requested_by = dr["requested_by"].ToString(),
                    maintenance_by = dr["maintenance_by"].ToString(),
                    schedule_date = Convert.ToDateTime(dr["schedule_date"]).ToString("dd-MM-yyyy"),
                    start_date = Convert.ToDateTime(dr["start_date"]).ToString("dd-MM-yyyy"),
                    finish_date = Convert.ToDateTime(dr["finish_date"]).ToString("dd-MM-yyyy"),
                    maintenance_cost = Convert.ToInt32(dr["maintenance_cost"]),
                    description = dr["description"].ToString(),
                    desc_maintenance = dr["desc_maintenance"].ToString(),
                    status = dr["status"].ToString(),
                });
            };
            dr.Close();
            con.Close();
            return wop;
        }

        public List<MaintenancePreventiveModel> getAllWOPMachine(string id_machine) // ini buat ngambil semua data sparepart pada mesin
        {
            List<MaintenancePreventiveModel> wop = new List<MaintenancePreventiveModel>();
            SqlCommand cmd = new SqlCommand("select * from WO_Preventive where id_machine = @id_machine and status <> @status order by id_wop desc", con);
            cmd.Parameters.AddWithValue("@id_machine", id_machine);
            cmd.Parameters.AddWithValue("@status", "Finished");
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                wop.Add(new MaintenancePreventiveModel()
                {
                    id_wop = dr["id_wop"].ToString(),
                    id_machine = dr["id_machine"].ToString(),
                    id_callendar = Convert.ToInt32(dr["id_callendar"]),
                    requested_by = dr["requested_by"].ToString(),
                    maintenance_by = dr["maintenance_by"].ToString(),
                    schedule_date = Convert.ToDateTime(dr["schedule_date"]).ToString("dd-MM-yyyy"),
                    start_date = Convert.ToDateTime(dr["start_date"]).ToString("dd-MM-yyyy"),
                    finish_date = Convert.ToDateTime(dr["finish_date"]).ToString("dd-MM-yyyy"),
                    maintenance_cost = Convert.ToInt32(dr["maintenance_cost"]),
                    sparepart_cost = Convert.ToInt32(dr["sparepart_cost"]),
                    cost = Convert.ToInt32(dr["cost"]),
                    description = dr["description"].ToString(),
                    desc_maintenance = dr["desc_maintenance"].ToString(),
                    status = dr["status"].ToString(),
                });
            };
            dr.Close();
            con.Close();
            return wop;
        }

        public MaintenancePreventiveModel getData(string id)
        {
            MaintenancePreventiveModel wop = new MaintenancePreventiveModel();
            SqlCommand cmd = new SqlCommand("select * from WO_Preventive where id_wop = @id_wop order by id_wop desc", con);
            cmd.Parameters.AddWithValue("@id_wop", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            wop.id_wop = dr["id_wop"].ToString();
            wop.id_machine = dr["id_machine"].ToString();
            wop.id_callendar = Convert.ToInt32(dr["id_callendar"]);
            wop.requested_by = dr["requested_by"].ToString();
            wop.maintenance_by = dr["maintenance_by"].ToString();
            wop.schedule_date = Convert.ToDateTime(dr["schedule_date"]).ToString("dd-MM-yyyy");
            wop.start_date = Convert.ToDateTime(dr["start_date"]).ToString("dd-MM-yyyy");
            wop.finish_date = Convert.ToDateTime(dr["finish_date"]).ToString("dd-MM-yyyy");
            wop.maintenance_cost = Convert.ToInt32(dr["maintenance_cost"]);
            wop.sparepart_cost = Convert.ToInt32(dr["sparepart_cost"]);
            wop.cost = Convert.ToInt32(dr["cost"]);
            wop.description = dr["description"].ToString();
            wop.desc_maintenance = dr["desc_maintenance"].ToString();
            wop.status = dr["status"].ToString();
            dr.Close();
            con.Close();
            return wop;
        }

        public List<MaintenancePreventiveModel> getMyWOP(string id_user) // ini buat ngambil semua data sparepart pada mesin
        {
            List<MaintenancePreventiveModel> wop = new List<MaintenancePreventiveModel>();
            SqlCommand cmd = new SqlCommand("select * from WO_Preventive where (status = @status OR status = @status2) and maintenance_by = @maintenance_by order by id_wop desc", con);
            cmd.Parameters.AddWithValue("@status", "Maintenance");
            cmd.Parameters.AddWithValue("@status2", "Remaintenance");
            cmd.Parameters.AddWithValue("@maintenance_by", id_user);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                wop.Add(new MaintenancePreventiveModel()
                {
                    id_wop = dr["id_wop"].ToString(),
                    id_machine = dr["id_machine"].ToString(),
                    id_callendar = Convert.ToInt32(dr["id_callendar"]),
                    requested_by = dr["requested_by"].ToString(),
                    maintenance_by = dr["maintenance_by"].ToString(),
                    schedule_date = Convert.ToDateTime(dr["schedule_date"]).ToString("dd-MM-yyyy"),
                    start_date = Convert.ToDateTime(dr["start_date"]).ToString("dd-MM-yyyy"),
                    finish_date = Convert.ToDateTime(dr["finish_date"]).ToString("dd-MM-yyyy"),
                    maintenance_cost = Convert.ToInt32(dr["maintenance_cost"]),
                    description = dr["description"].ToString(),
                    desc_maintenance = dr["desc_maintenance"].ToString(),
                    status = dr["status"].ToString(),
                });
            };
            dr.Close();
            con.Close();
            return wop;
        }

        //==============================================================================================================================================//
        public string getWorkOrderID(string key)
        {
            string id = key.Substring(0, key.IndexOf("/"));

            return id;
        }

        public string setWorkOrderPreventiveID(string key)
        {
            SqlCommand cmd = new SqlCommand("select * from(select left(id_wop, charindex('/', id_wop)-1) as [key],id_wop FROM WO_Preventive) a where [key] = @key", con);
            cmd.Parameters.AddWithValue("@key", key);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            string id = dr["id_wop"].ToString();
            dr.Close();
            con.Close();
            return id;
        }


        public Boolean maintenanceupdate(MaintenancePreventiveModel maintenancePreventiveModel) // ini buat insert data user
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_updatemaintenancewop", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_wop", maintenancePreventiveModel.id_wop);
                cmd.Parameters.AddWithValue("@maintenance_cost", maintenancePreventiveModel.maintenance_cost);
                cmd.Parameters.AddWithValue("@desc_maintenance", maintenancePreventiveModel.desc_maintenance);
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
                SqlCommand cmd = new SqlCommand("sp_finishmaintenancewop", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_wop", id);
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

        public Boolean insert_sparepartwop(string id_wop, string quantity, int cost, string id_sparepart) // ini buat insert sparepart machine
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_insertsparepartwop", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_wop", id_wop);
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

        public Boolean delete_sparepartmachinewop(string id_wop, string id_sparepart) //Hapus foto mesin dari detail mesin
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_deletesparepartwop", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_wop", id_wop);
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


    }
}