 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CMMS.Models
{
    public class Preventive
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        Callendar _callendar = new Callendar();

        public List<PreventiveModel> getAllWOPreventiveUser(string id_user)
        {
            List<PreventiveModel> woPreventiveModels = new List<PreventiveModel>();

            SqlCommand cmd = new SqlCommand("select * from WO_Preventive where requested_by = @id_user order by id_wop desc", con);
            cmd.Parameters.AddWithValue("@id_user", id_user);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                woPreventiveModels.Add(new PreventiveModel()
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


            return woPreventiveModels;
        }

        public string idwopreventive() //Get last ID Callendar
        {
            string id = "";
            string month = DateTime.Now.ToString("MM");
            string year = DateTime.Now.ToString("yyyy");

            switch (month)
            {
                case "01":
                    month = "I";
                    break;
                case "02":
                    month = "II";
                    break;
                case "03":
                    month = "III";
                    break;
                case "04":
                    month = "IV";
                    break;
                case "05":
                    month = "V";
                    break;
                case "06":
                    month = "VI";
                    break;
                case "07":
                    month = "VII";
                    break;
                case "08":
                    month = "VIII";
                    break;
                case "09":
                    month = "IX";
                    break;
                case "10":
                    month = "X";
                    break;
                case "11":
                    month = "XI";
                    break;
                case "12":
                    month = "XII";
                    break;
            }

            SqlCommand cmd = new SqlCommand("SELECT TOP(1) LEFT(id_wop, CHARINDEX('/', id_wop) - 1) + 1 as lastid FROM WO_Preventive ORDER BY lastid desc", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (dr["lastid"] != DBNull.Value)
                {
                    id = dr["lastid"].ToString() + "/WO/PM/" + month + "/" + year;
                }
                else
                {
                    id = "1/WO/PM/" + month + "/" + year;
                }
            }
            else
            {
                id = "1/WO/PM/" + month + "/" + year;
            }

            dr.Close();
            con.Close();
            return id;
        }

        public Boolean wopinsert(PreventiveModel woPreventiveModel) // ini buat insert data
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_insertwop", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_wop", idwopreventive());
                cmd.Parameters.AddWithValue("@id_machine", woPreventiveModel.id_machine);
                cmd.Parameters.AddWithValue("@requested_by", woPreventiveModel.requested_by);
                cmd.Parameters.AddWithValue("@description", woPreventiveModel.description);
                cmd.Parameters.AddWithValue("@status", woPreventiveModel.status);
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
        //Ending   Work Order Preventif



        //===========================================================================================================================//

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

        public Boolean isDataWOP(string id) // ini buat ngecheck datanya aktif atau belom kena delete
        {
            SqlCommand cmd = new SqlCommand("select * from WO_Preventive where id_wop like @id and status like @status", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@status", "Draft");
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

        public PreventiveModel getDataWOPreventiveUser(string id)
        {
            PreventiveModel woPreventiveModel = new PreventiveModel();
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


        public Boolean wopupdate(PreventiveModel woPreventiveModel) // ini buat insert data user
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_updatewop", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_wop", woPreventiveModel.id_wop);
                cmd.Parameters.AddWithValue("@id_machine", woPreventiveModel.id_machine);
                cmd.Parameters.AddWithValue("@description", woPreventiveModel.description);
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

        public Boolean wopdelete(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_deletewop", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_wop", id);
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

        public Boolean wop_acc(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_wop_acc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_wop", id);
                cmd.Parameters.AddWithValue("@status", "Approved");
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

        public Boolean wop_acc2(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_wop_acc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_wop", id);
                cmd.Parameters.AddWithValue("@status", "Finished");
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

        public Boolean wop_acc3(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_wop_acc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_wop", id);
                cmd.Parameters.AddWithValue("@status", "Checking UPT");
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

        public Boolean wop_reject1(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_wop_acc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_wop", id);
                cmd.Parameters.AddWithValue("@status", "Draft");
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

        public Boolean wop_reject2(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_wop_acc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_wop", id);
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

        public Boolean wopsend(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_sendwop", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_wop", id);
                cmd.Parameters.AddWithValue("@schedule_date", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@status", "Submitted");
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