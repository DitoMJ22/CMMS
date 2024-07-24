using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CMMS.Models
{
    public class AdminCorrective
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        Callendar _callendar = new Callendar();

        public List<AdminCorrectiveModel> getAllApproved() // ini buat ngambil semua data WOC dengan status Accepted
        {
            List<AdminCorrectiveModel> woc = new List<AdminCorrectiveModel>();
            SqlCommand cmd = new SqlCommand("select * from WO_Corrective order by id_woc desc", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                woc.Add(new AdminCorrectiveModel()
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

        public string getWorkOrderID(string key)
        {
            string id = key.Substring(0, key.IndexOf("/"));

            return id;
        }

        public string setWOCID(string key)
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
        public AdminCorrectiveModel getDataWOCorrectiveUser(string id)
        {
            AdminCorrectiveModel woCorrectiveModel = new AdminCorrectiveModel();
            SqlCommand cmd = new SqlCommand("select * from WO_Corrective where id_woc like @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            woCorrectiveModel.id_woc = dr["id_woc"].ToString();
            woCorrectiveModel.id_machine = dr["id_machine"].ToString();
            woCorrectiveModel.id_callendar = Convert.ToInt32(dr["id_callendar"]);
            woCorrectiveModel.requested_by = dr["requested_by"].ToString();
            woCorrectiveModel.maintenance_by = dr["maintenance_by"].ToString();
            woCorrectiveModel.request_date = Convert.ToDateTime(dr["request_date"]).ToString("dd-MM-yyyy");
            woCorrectiveModel.deadline = Convert.ToDateTime(dr["start_date"]).ToString("dd-MM-yyyy");
            woCorrectiveModel.finish_date = Convert.ToDateTime(dr["finish_date"]).ToString("dd-MM-yyyy");
            woCorrectiveModel.maintenance_cost = Convert.ToInt32(dr["maintenance_cost"]);
            woCorrectiveModel.desc_request = dr["desc_request"].ToString();
            woCorrectiveModel.desc_maintenance = dr["desc_maintenance"].ToString();
            woCorrectiveModel.status = dr["status"].ToString();
            dr.Close();
            con.Close();
            return woCorrectiveModel;
        }

        public AdminCorrectiveModel getData(string id)
        {
            AdminCorrectiveModel woc = new AdminCorrectiveModel();
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
            woc.deadline = Convert.ToDateTime(dr["start_date"]).ToString("dd-MM-yyyy");
            woc.finish_date = Convert.ToDateTime(dr["finish_date"]).ToString("dd-MM-yyyy");
            woc.maintenance_cost = Convert.ToInt32(dr["maintenance_cost"]);
            woc.desc_request = dr["desc_request"].ToString();
            woc.desc_maintenance = dr["desc_maintenance"].ToString();
            woc.status = dr["status"].ToString();
            dr.Close();
            con.Close();
            return woc;
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

        //update
        public Boolean update(AdminCorrectiveModel adminCorrectiveModel) // ini buat insert data user
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_Ploting", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_woc", adminCorrectiveModel.id_woc);
                cmd.Parameters.AddWithValue("@maintenance_by", adminCorrectiveModel.maintenance_by);
                cmd.Parameters.AddWithValue("@start_date", adminCorrectiveModel.deadline);
                cmd.Parameters.AddWithValue("@status", "Maintenance");
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

        public string setWorkOrderPICName(string maintenanceBy)
        {
            SqlCommand cmd = new SqlCommand("select * from user where name = @maintenanceBy", con);
            cmd.Parameters.AddWithValue("@maintenanceBy", maintenanceBy);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            string name = dr["name"].ToString();
            dr.Close();
            con.Close();
            return name;
        }

        public Boolean woc_plot(string id, string maintenanceBy, string start_date)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_woc_plot", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_woc", id);
                cmd.Parameters.AddWithValue("@id_callendar", _callendar.idcallendar());
                cmd.Parameters.AddWithValue("@maintenance_by", maintenanceBy);
                cmd.Parameters.AddWithValue("@deadline", Convert.ToDateTime(start_date));
                cmd.Parameters.AddWithValue("@status", "Maintenance");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                SqlCommand cmd1 = new SqlCommand("sp_insertNotifikasiTopBar", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@id_notification", id);
                cmd1.Parameters.AddWithValue("@title", "New Plotting Corrective Work Order Has Been Added For You");
                cmd1.Parameters.AddWithValue("@description", "Please immidiately carry out the maintenance and upload the maintenance");
                cmd1.Parameters.AddWithValue("@received_by", maintenanceBy);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();

                SqlCommand cmd2 = new SqlCommand("sp_updateNotifikasiTopBar", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@id_notification", id);
                cmd2.Parameters.AddWithValue("@title", "New Corrective Work Order is Accepted By Kepala UPT");
                con.Open();
                cmd2.ExecuteNonQuery();
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