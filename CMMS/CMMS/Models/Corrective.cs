using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CMMS.Models
{
    public class Corrective
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        Callendar _callendar = new Callendar();

        public List<CorrectiveModel> getAllWOCorrectiveUser(string id_user)
        {
            List<CorrectiveModel> woCorrectiveModels = new List<CorrectiveModel>();

            SqlCommand cmd = new SqlCommand("select * from WO_Corrective where requested_by = @id_user order by id_woc desc", con);
            cmd.Parameters.AddWithValue("@id_user", id_user);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                woCorrectiveModels.Add(new CorrectiveModel()
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


            return woCorrectiveModels;
        }

        public string idwocorrective() //Get last ID Callendar
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

            SqlCommand cmd = new SqlCommand("SELECT TOP(1) LEFT(id_woc, CHARINDEX('/', id_woc) - 1) + 1 as lastid FROM WO_Corrective ORDER BY lastid desc", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (dr["lastid"] != DBNull.Value)
                {
                    id = dr["lastid"].ToString() + "/WO/CM/" + month + "/" + year;
                }
                else
                {
                    id = "1/WO/CM/" + month + "/" + year;
                }
            }
            else
            {
                id = "1/WO/CM/" + month + "/" + year;
            }

            dr.Close();
            con.Close();
            return id;
        }

        public Boolean wocinsert(CorrectiveModel woCorrectiveModel) // ini buat insert data
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_insertwoc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_woc", idwocorrective());
                cmd.Parameters.AddWithValue("@id_machine", woCorrectiveModel.id_machine);
                cmd.Parameters.AddWithValue("@requested_by", woCorrectiveModel.requested_by);
                cmd.Parameters.AddWithValue("@desc_request", woCorrectiveModel.desc_request);
                cmd.Parameters.AddWithValue("@status", woCorrectiveModel.status);
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

        public string setWorkOrderPreventifID(string key)
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

        public Boolean isDataWOC(string id) // ini buat ngecheck datanya aktif atau belom kena delete
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

        public CorrectiveModel getDataWOCorrectiveUser(string id)
        {
            CorrectiveModel woCorrectiveModel = new CorrectiveModel();
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
            woCorrectiveModel.deadline = Convert.ToDateTime(dr["deadline"]).ToString("dd-MM-yyyy");
            woCorrectiveModel.finish_date = Convert.ToDateTime(dr["finish_date"]).ToString("dd-MM-yyyy");
            woCorrectiveModel.maintenance_cost = Convert.ToInt32(dr["maintenance_cost"]);
            woCorrectiveModel.sparepart_cost = Convert.ToInt32(dr["sparepart_cost"]);
            woCorrectiveModel.cost = Convert.ToInt32(dr["cost"]);
            woCorrectiveModel.desc_request = dr["desc_request"].ToString();
            woCorrectiveModel.desc_maintenance = dr["desc_maintenance"].ToString();
            woCorrectiveModel.status = dr["status"].ToString();
            dr.Close();
            con.Close();
            return woCorrectiveModel;
        }


        public Boolean wocupdate(CorrectiveModel woCorrectiveModel) // ini buat insert data user
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_updatewoc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_woc", woCorrectiveModel.id_woc);
                cmd.Parameters.AddWithValue("@id_machine", woCorrectiveModel.id_machine);
                cmd.Parameters.AddWithValue("@desc_request", woCorrectiveModel.desc_request);
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

        public Boolean wocdelete(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_deletewoc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_woc", id);
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

        public Boolean woc_acc(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_woc_acc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_woc", id);
                cmd.Parameters.AddWithValue("@status", "Approved");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                SqlCommand cmd1 = new SqlCommand("sp_insertNotifikasiTopBarMain", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@id_notification", id);
                cmd1.Parameters.AddWithValue("@title", "New Corrective Work Order is Accepted By Kepala UPT");
                cmd1.Parameters.AddWithValue("@description", "Please see the newly approved corrective work order submission");
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();

                SqlCommand cmd2 = new SqlCommand("sp_updateNotifikasiTopBar", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@id_notification", id);
                cmd2.Parameters.AddWithValue("@title", "New Corrective Work Order is Submitted");
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

        public Boolean woc_acc2(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_woc_acc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_woc", id);
                cmd.Parameters.AddWithValue("@status", "Finished");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                SqlCommand cmd2 = new SqlCommand("sp_updateNotifikasiTopBar", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@id_notification", id);
                cmd2.Parameters.AddWithValue("@title", "Corrective Work Order Report Has Been Accepted by Kepala Maintenance");
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

        public Boolean woc_acc3(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_woc_acc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_woc", id);
                cmd.Parameters.AddWithValue("@status", "Checking UPT");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                SqlCommand cmd1 = new SqlCommand("sp_insertNotifikasiTopBarPIC", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@id_notification", id);
                cmd1.Parameters.AddWithValue("@title", "Corrective Work Order Report Has Been Accepted by Kepala Maintenance");
                cmd1.Parameters.AddWithValue("@description", "Please check the report that has been submitted");
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();

                SqlCommand cmd2 = new SqlCommand("sp_updateNotifikasiTopBar", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@id_notification", id);
                cmd2.Parameters.AddWithValue("@title", "Corrective Work Order Report Has Been Submitted");
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

        public Boolean woc_reject1(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_woc_acc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_woc", id);
                cmd.Parameters.AddWithValue("@status", "Draft");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                SqlCommand cmd2 = new SqlCommand("sp_updateNotifikasiTopBar", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@id_notification", id);
                cmd2.Parameters.AddWithValue("@title", "New Corrective Work Order is Submitted");
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

        public Boolean woc_reject2(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_woc_acc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_woc", id);
                cmd.Parameters.AddWithValue("@status", "Maintenance");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                SqlCommand cmd1 = new SqlCommand("sp_insertNotifikasiTopBarMain2", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@id_notification", id);
                cmd1.Parameters.AddWithValue("@title", "Corrective Work Order Report Has Been Rejected by Kepala Maintenance");
                cmd1.Parameters.AddWithValue("@description", "Please reupload the report that has been rejected");
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();

                SqlCommand cmd2 = new SqlCommand("sp_updateNotifikasiTopBar", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@id_notification", id);
                cmd2.Parameters.AddWithValue("@title", "Corrective Work Order Report Has Been Submitted");
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

        public Boolean woc_reject3(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_woc_acc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_woc", id);
                cmd.Parameters.AddWithValue("@status", "Maintenance");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                SqlCommand cmd1 = new SqlCommand("sp_insertNotifikasiTopBarMain2", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@id_notification", id);
                cmd1.Parameters.AddWithValue("@title", "Corrective Work Order Report Has Been Rejected by PIC Maintenance");
                cmd1.Parameters.AddWithValue("@description", "Please reupload the report that has been rejected");
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();

                SqlCommand cmd2 = new SqlCommand("sp_updateNotifikasiTopBar", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@id_notification", id);
                cmd2.Parameters.AddWithValue("@title", "Corrective Work Order Report Has Been Rejected by Kepala Maintenance");
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

        public Boolean wocsend(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_sendwoc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_woc", id);
                cmd.Parameters.AddWithValue("@request_date", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@status", "Submitted");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                SqlCommand cmd1 = new SqlCommand("sp_insertNotifikasiTopBarUPT", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@id_notification", id);
                cmd1.Parameters.AddWithValue("@title", "New Corrective Work Order is Submitted");
                cmd1.Parameters.AddWithValue("@description", "Please see the recently added corrective work order submission");
                con.Open();
                cmd1.ExecuteNonQuery();
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