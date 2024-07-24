using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CMMS.Models;

namespace CMMS.Controllers
{
    public class AJAXController : Controller
    {
        // GET: AJAX
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        Report _report = new Report();

        public string Callendar()
        {
            List<CallendarModel> callendarModel = new List<CallendarModel>();

            SqlCommand cmd = new SqlCommand("Select * from Callendar", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                callendarModel.Add(new CallendarModel()
                {
                    id = Convert.ToInt32(dr["id"]),
                    title = dr["title"].ToString(),
                    description = dr["description"].ToString(),
                    start = Convert.ToDateTime(dr["start"]).ToString("yyyy-MM-dd dd:mm:hh"),
                    end = Convert.ToDateTime(dr["end"]).ToString("yyyy-MM-dd dd:mm:hh")
                });
            };
            dr.Close();
            con.Close();

            var data = callendarModel;

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string strJSON = jss.Serialize(data);
            return strJSON;
        }

        public string ChartSensorAll()
        {
            List<ChartModel> chartModel = new List<ChartModel>();

            SqlCommand cmd = new SqlCommand("SELECT * FROM machine WHERE no_asset IN (SELECT DISTINCT id_machine FROM sensor)", con);
            SqlCommand cmd1 = new SqlCommand("SELECT Sensor.temperature, Sensor.id_machine, Machine.name FROM Sensor INNER JOIN Machine ON Sensor.id_machine = Machine.no_asset", con);

            //con.Open();

            //// Ambil data mesin dari tabel 'machine'
            //SqlDataReader dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    chartModel.Add(new ChartModel()
            //    {
            //        id = dr["no_asset"].ToString(),
            //        name = dr["name"].ToString(),
            //        temperatures = new List<float>()
            //    });
            //}
            //dr.Close();

            //// Ambil data suhu dari tabel 'sensor'
            //SqlDataReader dr1 = cmd1.ExecuteReader();
            //while (dr1.Read())
            //{
            //    string idMachine = Convert.ToString(dr1["id_machine"]);

            //    foreach (ChartModel model in chartModel)
            //    {
            //        if (model.id.Equals(idMachine))
            //        {
            //            model.temperature = Convert.ToSingle(dr1["temperature"]);
            //            model.temperatures.Add(model.temperature);
            //        }
            //    }
            //}
            //dr1.Close();

            //con.Close();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string strJSON = jss.Serialize(chartModel);
            return strJSON;
        }

        public string ChartSensor(string no_asset)
        {
            List<ChartModel> chartModel = new List<ChartModel>();

            SqlCommand cmd = new SqlCommand("SELECT TOP 20 Sensor.temperature, Machine.name, Sensor.waktu FROM Sensor " +
                                            "INNER JOIN Machine ON Sensor.id_machine = Machine.no_asset WHERE Machine.no_asset = @no_asset ORDER BY Sensor.id_monitoring DESC", con);
            cmd.Parameters.AddWithValue("@no_asset", no_asset);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chartModel.Add(new ChartModel()
                {
                    name = dr["name"].ToString(),
                    waktu = Convert.ToDateTime(dr["waktu"]), // Ubah data waktu ke tipe data DateTime
                    temperature = Convert.ToInt32(dr["temperature"])
                });
            }
            dr.Close();
            con.Close();

            var data = chartModel;

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string strJSON = jss.Serialize(data);
            return strJSON;
        }


        public string WorkOrder()
        {

            List<ChartAdminModel> chartAdminModel = new List<ChartAdminModel>();
            SqlCommand cmd = new SqlCommand("select * from upt", con);
            SqlCommand cmd1 = new SqlCommand("SELECT COUNT(WO_corrective.id_woc) as TotalWOC, Upt.name as UPT FROM WO_corrective INNER JOIN Machine ON WO_corrective.id_machine = Machine.no_asset " +
                "INNER JOIN Lab ON Machine.lab = Lab.id INNER JOIN UPT ON Lab.upt = UPT.id WHERE WO_corrective.status = 'Finished' GROUP BY Upt.name", con);
            SqlCommand cmd2 = new SqlCommand("SELECT COUNT(WO_Preventive.id_wop) as TotalWOP, Upt.name as UPT FROM WO_Preventive INNER JOIN Machine ON WO_Preventive.id_machine = Machine.no_asset " +
                "INNER JOIN Lab ON Machine.lab = Lab.id INNER JOIN UPT ON Lab.upt = UPT.id WHERE WO_Preventive.status = 'Finished' GROUP BY Upt.name", con);
            SqlCommand cmd3 = new SqlCommand("SELECT COUNT(WO_Corrective.id_woc) AS TotalWOC, upt.name AS UPT, SUM(COALESCE(WO_Corrective.Cost, 0)) AS TotalCostC FROM WO_Corrective INNER JOIN machine ON WO_Corrective.id_machine = machine.no_asset INNER JOIN lab ON machine.lab = lab.id " +
                "INNER JOIN upt ON lab.id = upt.id WHERE WO_Corrective.status = 'Finished' GROUP BY upt.name", con);
            SqlCommand cmd4 = new SqlCommand("SELECT COUNT(WO_Preventive.id_wop) AS TotalWOP, upt.name AS UPT, SUM(COALESCE(WO_Preventive.Cost, 0)) AS TotalCostP FROM WO_Preventive INNER JOIN machine ON WO_Preventive.id_machine = machine.no_asset INNER JOIN lab ON machine.lab = lab.id " +
                "INNER JOIN upt ON lab.id = upt.id WHERE WO_Preventive.status = 'Finished' GROUP BY upt.name", con);

            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chartAdminModel.Add(new ChartAdminModel()
                {
                    name = Convert.ToString(dr["name"]),
                });
            }
            dr.Close();

            SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                string nameTemp = Convert.ToString(dr1["UPT"]);

                foreach (ChartAdminModel model in chartAdminModel)
                {
                    if (model.name.Equals(nameTemp))
                    {
                        model.dataWoc += Convert.ToInt32(dr1["TotalWOC"]);
                    }
                }

            }
            dr1.Close();

            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                string nameTemp = Convert.ToString(dr2["UPT"]);

                foreach (ChartAdminModel model in chartAdminModel)
                {
                    if (model.name.Equals(nameTemp))
                    {
                        model.dataWop += Convert.ToInt32(dr2["TotalWOP"]); ;
                    }
                }

            }
            dr2.Close();

            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                string nameTemp = Convert.ToString(dr3["UPT"]);

                foreach (ChartAdminModel model in chartAdminModel)
                {
                    if (model.name.Equals(nameTemp))
                    {
                        model.dataWocCost += Convert.ToInt32(dr3["TotalCostC"]); ;
                    }
                }

            }
            dr3.Close();

            SqlDataReader dr4 = cmd4.ExecuteReader();
            while (dr4.Read())
            {
                string nameTemp = Convert.ToString(dr4["UPT"]);

                foreach (ChartAdminModel model in chartAdminModel)
                {
                    if (model.name.Equals(nameTemp))
                    {
                        model.dataWopCost += Convert.ToInt32(dr4["TotalCostP"]); ;
                    }
                }

            }
            dr4.Close();

            con.Close();

            var data = chartAdminModel;

            JavaScriptSerializer jss = new JavaScriptSerializer();

            string strJSON = jss.Serialize(data);
            return strJSON;
        }

        public string WorkOrderSort(string endDate, string startDate)
        {

            List<ChartAdminModel> chartAdminModel = new List<ChartAdminModel>();
            SqlCommand cmd = new SqlCommand("select * from upt", con);
            SqlCommand cmd1 = new SqlCommand("SELECT COUNT(WO_corrective.id_woc) as TotalWOC, Upt.name as UPT FROM WO_corrective INNER JOIN Machine ON WO_corrective.id_machine = Machine.no_asset INNER JOIN Lab ON Machine.lab = Lab.id " +
                "INNER JOIN UPT ON Lab.upt = UPT.id WHERE WO_corrective.status = 'Finished' AND WO_corrective.finish_date >= '" + startDate + "' AND WO_corrective.finish_date <= '" + endDate + "' GROUP BY Upt.name", con);
            SqlCommand cmd2 = new SqlCommand("SELECT COUNT(WO_Preventive.id_wop) as TotalWOP, Upt.name as UPT FROM WO_Preventive INNER JOIN Machine ON WO_Preventive.id_machine = Machine.no_asset INNER JOIN Lab ON Machine.lab = Lab.id " +
                "INNER JOIN UPT ON Lab.upt = UPT.id WHERE WO_Preventive.status = 'Finished' AND WO_Preventive.finish_date >= '" + startDate + "' AND WO_Preventive.finish_date <= '" + endDate + "' GROUP BY Upt.name", con);
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chartAdminModel.Add(new ChartAdminModel()
                {
                    name = Convert.ToString(dr["name"]),
                });
            }
            dr.Close();

            SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                string nameTemp = Convert.ToString(dr1["UPT"]);

                foreach (ChartAdminModel model in chartAdminModel)
                {
                    if (model.name.Equals(nameTemp))
                    {
                        model.dataWoc += Convert.ToInt32(dr1["TotalWOC"]);
                    }
                }

            }
            dr1.Close();

            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                string nameTemp = Convert.ToString(dr2["UPT"]);

                foreach (ChartAdminModel model in chartAdminModel)
                {
                    if (model.name.Equals(nameTemp))
                    {
                        model.dataWop += Convert.ToInt32(dr2["TotalWOP"]); ;
                    }
                }

            }
            dr2.Close();

            con.Close();



            var data = chartAdminModel;

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string strJSON = jss.Serialize(data);
            return strJSON;
        }


        [HttpPost]
        public JsonResult countAllWOSort_Ctrlr(string endDate, string startDate)
        {
            // Implementasi untuk memanggil method countAllWO_Cost() pada model _report.
            var result = _report.countAllWOSort(endDate, startDate);

            return Json(result);
        }

        [HttpPost]
        public JsonResult countAllCostSort_Ctrlr(string endDate, string startDate)
        {
            // Implementasi untuk memanggil method countAllWO_Cost() pada model _report.
            var result = _report.countAllWO_CostSort(endDate, startDate);

            return Json(result);
        }

    }
}