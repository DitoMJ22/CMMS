using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CMMS.Models
{
    public class Notification
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public List<NotificationModel> getAllData(string id_user) // ini buat ngambil semua data user
        {
            List<NotificationModel> notifs = new List<NotificationModel>();
            SqlCommand cmd = new SqlCommand("Select * from Notification_TopBar where status = 1 AND received_by = @receive", con);
            cmd.Parameters.AddWithValue("@receive", id_user);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                notifs.Add(new NotificationModel()
                {
                    id_notification = dr["id_notification"].ToString(),
                    title = dr["title"].ToString(),
                    description = dr["description"].ToString(),
                    received_by = dr["received_by"].ToString(),
                    date = dr["date"].ToString(),
                    status = dr["status"].ToString(),
                });
            };
            dr.Close();
            con.Close();
            return notifs;
        }
    }
}