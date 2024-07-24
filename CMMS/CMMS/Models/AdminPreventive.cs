using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CMMS.Models
{
    public class AdminPreventive
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        Callendar _callendar = new Callendar();

        public List<AdminPreventiveModel> getAllApproved() // ini buat ngambil semua data wop dengan status Accepted
        {
            List<AdminPreventiveModel> wop = new List<AdminPreventiveModel>();
            SqlCommand cmd = new SqlCommand("select * from WO_Preventive order by id_wop desc", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                wop.Add(new AdminPreventiveModel()
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

        public string getWorkOrderID(string key)
        {
            string id = key.Substring(0, key.IndexOf("/"));

            return id;
        }

        public string setWOPID(string key)
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
        public AdminPreventiveModel getDataWOPreventiveUser(string id)
        {
            AdminPreventiveModel woPreventiveModel = new AdminPreventiveModel();
            SqlCommand cmd = new SqlCommand("select * from WO_Preventive where id_wop like @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            woPreventiveModel.id_wop = dr["id_wop"].ToString();
            woPreventiveModel.id_machine = dr["id_machine"].ToString();
            woPreventiveModel.id_callendar = Convert.ToInt32(dr["id_callendar"]);
            woPreventiveModel.requested_by = dr["requested_by"].ToString();
            woPreventiveModel.maintenance_by = dr["maintenance_by"].ToString();
            woPreventiveModel.schedule_date = Convert.ToDateTime(dr["schedule_date"]).ToString("dd-MM-yyyy");
            woPreventiveModel.start_date = Convert.ToDateTime(dr["start_date"]).ToString("dd-MM-yyyy");
            woPreventiveModel.finish_date = Convert.ToDateTime(dr["finish_date"]).ToString("dd-MM-yyyy");
            woPreventiveModel.maintenance_cost = Convert.ToInt32(dr["maintenance_cost"]);
            woPreventiveModel.description = dr["description"].ToString();
            woPreventiveModel.desc_maintenance = dr["desc_maintenance"].ToString();
            woPreventiveModel.status = dr["status"].ToString();
            dr.Close();
            con.Close();
            return woPreventiveModel;
        }

        public AdminPreventiveModel getData(string id)
        {
            AdminPreventiveModel wop = new AdminPreventiveModel();
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
            wop.description = dr["description"].ToString();
            wop.desc_maintenance = dr["desc_maintenance"].ToString();
            wop.status = dr["status"].ToString();
            dr.Close();
            con.Close();
            return wop;
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

        //update
        public Boolean update(AdminPreventiveModel AdminPreventiveModel) // ini buat insert data user
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_wop_plot", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_wop", AdminPreventiveModel.id_wop);
                cmd.Parameters.AddWithValue("@maintenance_by", AdminPreventiveModel.maintenance_by);
                cmd.Parameters.AddWithValue("@start_date", AdminPreventiveModel.start_date);
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

        public Boolean wop_plot(string id, string maintenanceBy, string start_date)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_wop_plot", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_wop", id);
                cmd.Parameters.AddWithValue("@id_callendar", _callendar.idcallendar());
                cmd.Parameters.AddWithValue("@maintenance_by", maintenanceBy);
                cmd.Parameters.AddWithValue("@start_date", Convert.ToDateTime(start_date));
                cmd.Parameters.AddWithValue("@status", "Maintenance");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                SqlCommand cmd1 = new SqlCommand("sp_insertNotifikasiTopBar", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@id_notification", id);
                cmd1.Parameters.AddWithValue("@title", "New Plotting Preventive Work Order Has Been Added For You");
                cmd1.Parameters.AddWithValue("@description", "Please immidiately carry out the maintenance and upload the maintenance");
                cmd1.Parameters.AddWithValue("@received_by", maintenanceBy);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();

                SqlCommand cmd2 = new SqlCommand("sp_updateNotifikasiTopBar", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@id_notification", id);
                cmd2.Parameters.AddWithValue("@title", "New Preventive is Accepted By Kepala UPT");
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