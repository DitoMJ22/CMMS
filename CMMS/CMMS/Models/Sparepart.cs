using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CMMS.Models
{
    public class Sparepart
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        // CRUD Needs
        public string idsparepart() //Get last ID Sparepart
        {
            string id = "";
            SqlCommand cmd = new SqlCommand("SELECT (IDENT_CURRENT('Sparepart') + 1) AS lastid", con);
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

        public string idphoto() //Get last ID Photo
        {
            string id = "";
            SqlCommand cmd = new SqlCommand("SELECT (IDENT_CURRENT('Detail_PhotoPart') + 1) AS lastid", con);
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

        public List<string> getAll_photoPart(string id_sparepart) //Get All Photo
        {
            List<string> photos = new List<string>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Detail_PhotoPart WHERE id_sparepart=@id_sparepart", con);
            cmd.Parameters.AddWithValue("@id_sparepart", id_sparepart);
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

        public Boolean delete_photoPart(string id) //Hapus foto part dari detail part
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spphotopartdelete", con);
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

        public Boolean isUniqueID(string id) // ini buat ngecheck no_asset nya udah unik apa belom , kalau tidak ditemukan return false dan sebaliknya
        {
            SqlCommand cmd = new SqlCommand("Select * from Sparepart where id like @id", con);
            cmd.Parameters.AddWithValue("@id", id);
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
            SqlCommand cmd = new SqlCommand("Select * from Sparepart where id like @id and status = @status", con);
            cmd.Parameters.AddWithValue("@id", id);
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

        public List<SparepartModel> getAllData() // ini buat ngambil semua data user
        {
            List<SparepartModel> spareparts = new List<SparepartModel>();
            SqlCommand cmd = new SqlCommand("Select * from Sparepart where status = @status", con);
            cmd.Parameters.AddWithValue("@status", 1);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                spareparts.Add(new SparepartModel()
                {
                    id = dr["id"].ToString(),
                    name = dr["name"].ToString(),
                    function = dr["function"].ToString(),
                    Stock = dr["stock"].ToString(),
                    unit = dr["unit"].ToString(),
                    price = dr["price"].ToString(),
                    status = dr["status"].ToString(),
                });
            };
            dr.Close();
            con.Close();
            return spareparts;
        }

        public List<SparepartModel> getAll_sparepartMachine(string id_machine) // ini buat ngambil semua data sparepart pada mesin
        {
            List<SparepartModel> spareparts = new List<SparepartModel>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Sparepart AS S JOIN Detail_Sparepart AS DS ON S.id = DS.id_sparepart WHERE DS.id_machine = @id_machine AND S.status = @status", con);
            cmd.Parameters.AddWithValue("@id_machine", id_machine);
            cmd.Parameters.AddWithValue("@status", 1);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                spareparts.Add(new SparepartModel()
                {
                    id = dr["id"].ToString(),
                    name = dr["name"].ToString(),
                    function = dr["function"].ToString(),
                    Stock = dr["stock"].ToString(),
                    unit = dr["unit"].ToString(),
                    price = dr["price"].ToString(),
                    status = dr["status"].ToString(),
                    quantity = dr["quantity"].ToString()
                });
            };
            dr.Close();
            con.Close();
            return spareparts;
        }

        public List<SparepartModel> getAll_sparepartMachineExcept(string id_machine) // ini buat ngambil semua data sparepart yang belum ada di mesin
        {
            List<SparepartModel> spareparts = new List<SparepartModel>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Sparepart WHERE status = @status and id NOT IN (SELECT ds.id_sparepart FROM Sparepart as s inner join Detail_Sparepart as ds on s.id = ds.id_sparepart where ds.id_machine = @id_machine)", con);
            cmd.Parameters.AddWithValue("@id_machine", id_machine);
            cmd.Parameters.AddWithValue("@status", 1);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                spareparts.Add(new SparepartModel()
                {
                    id = dr["id"].ToString(),
                    name = dr["name"].ToString(),
                    function = dr["function"].ToString(),
                    Stock = dr["stock"].ToString(),
                    unit = dr["unit"].ToString(),
                    price = dr["price"].ToString(),
                    status = dr["status"].ToString(),
                });
            };
            dr.Close();
            con.Close();
            return spareparts;
        }

        public SparepartModel getData(string id)
        {
            SparepartModel sparepart = new SparepartModel();
            SqlCommand cmd = new SqlCommand("Select * from Sparepart where id like @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            sparepart.id = dr["id"].ToString();
            sparepart.name = dr["name"].ToString();
            sparepart.function = dr["function"].ToString();
            sparepart.Stock = dr["stock"].ToString();
            sparepart.unit = dr["unit"].ToString();
            sparepart.price = dr["price"].ToString();
            sparepart.status = dr["status"].ToString();
            dr.Close();
            con.Close();
            return sparepart;
        }

        public SparepartModel getData2(string id)
        {
            SparepartModel sparepart = new SparepartModel();
            SqlCommand cmd = new SqlCommand("Select * from Sparepart where id like @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            sparepart.id = dr["id"].ToString();
            sparepart.name = dr["name"].ToString();
            sparepart.function = dr["function"].ToString();
            sparepart.Stock = dr["stock"].ToString();
            sparepart.unit = dr["unit"].ToString();
            sparepart.price = dr["price"].ToString().Replace(",0000", "");
            sparepart.status = dr["status"].ToString();
            dr.Close();
            con.Close();
            return sparepart;
        }

        //insert
        public Boolean insert(SparepartModel sparepartModel) // ini buat insert data user
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sppartinsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", sparepartModel.name);
                cmd.Parameters.AddWithValue("@function", sparepartModel.function);
                cmd.Parameters.AddWithValue("@stock", sparepartModel.Stock);
                cmd.Parameters.AddWithValue("@unit", sparepartModel.unit);
                cmd.Parameters.AddWithValue("@price", sparepartModel.price);
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
        public Boolean insert_photo(string id, string photo_name) // ini buat insert photo
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spphotopartinsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_sparepart", Convert.ToInt32(id));
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
        public Boolean update(SparepartModel sparepartModel) // ini buat insert data user
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sppartupdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", sparepartModel.id);
                cmd.Parameters.AddWithValue("@name", sparepartModel.name);
                cmd.Parameters.AddWithValue("@function", sparepartModel.function);
                cmd.Parameters.AddWithValue("@stock", sparepartModel.Stock);
                cmd.Parameters.AddWithValue("@unit", sparepartModel.unit);
                cmd.Parameters.AddWithValue("@price", sparepartModel.price);
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
                SqlCommand cmd = new SqlCommand("sppartdelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
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


        public List<SparepartModel> getAll_sparepartWOP(string id_wop) // ini buat ngambil semua data sparepart pada mesin
        {
            List<SparepartModel> spareparts = new List<SparepartModel>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Sparepart AS S JOIN WOP_Sparepart as WS on s.id = ws.id_sparepart where WS.id_wop = @id_wop and s.status = @status", con);
            cmd.Parameters.AddWithValue("@id_wop", id_wop);
            cmd.Parameters.AddWithValue("@status", 1);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                spareparts.Add(new SparepartModel()
                {
                    id = dr["id"].ToString(),
                    name = dr["name"].ToString(),
                    function = dr["function"].ToString(),
                    Stock = dr["stock"].ToString(),
                    unit = dr["unit"].ToString(),
                    price = dr["price"].ToString(),
                    status = dr["status"].ToString(),
                    quantity = dr["quantity"].ToString()
                });
            };
            dr.Close();
            con.Close();
            return spareparts;
        }

        public List<SparepartModel> getAll_sparepartWOPExcept(string id_wop) // ini buat ngambil semua data sparepart yang belum ada di mesin
        {
            List<SparepartModel> spareparts = new List<SparepartModel>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Sparepart WHERE status = @status and id NOT IN (SELECT ws.id_sparepart FROM Sparepart as s inner join WOP_Sparepart as ws on s.id = ws.id_sparepart where ws.id_wop = @id_wop)", con);
            cmd.Parameters.AddWithValue("@id_wop", id_wop);
            cmd.Parameters.AddWithValue("@status", 1);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                spareparts.Add(new SparepartModel()
                {
                    id = dr["id"].ToString(),
                    name = dr["name"].ToString(),
                    function = dr["function"].ToString(),
                    Stock = dr["stock"].ToString(),
                    unit = dr["unit"].ToString(),
                    price = dr["price"].ToString(),
                    status = dr["status"].ToString(),
                });
            };
            dr.Close();
            con.Close();
            return spareparts;
        }

        public List<SparepartModel> getAll_sparepartWOC(string id_woc) // ini buat ngambil semua data sparepart pada mesin
        {
            List<SparepartModel> spareparts = new List<SparepartModel>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Sparepart AS S JOIN WOC_Sparepart as WS on s.id = ws.id_sparepart where WS.id_woc = @id_woc and s.status = @status", con);
            cmd.Parameters.AddWithValue("@id_woc", id_woc);
            cmd.Parameters.AddWithValue("@status", 1);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                spareparts.Add(new SparepartModel()
                {
                    id = dr["id"].ToString(),
                    name = dr["name"].ToString(),
                    function = dr["function"].ToString(),
                    Stock = dr["stock"].ToString(),
                    unit = dr["unit"].ToString(),
                    price = dr["price"].ToString(),
                    status = dr["status"].ToString(),
                    quantity = dr["quantity"].ToString()
                });
            };
            dr.Close();
            con.Close();
            return spareparts;
        }

        public List<SparepartModel> getAll_sparepartWOCExcept(string id_woc) // ini buat ngambil semua data sparepart yang belum ada di mesin
        {
            List<SparepartModel> spareparts = new List<SparepartModel>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Sparepart WHERE status = @status and id NOT IN (SELECT ws.id_sparepart FROM Sparepart as s inner join WOC_Sparepart as ws on s.id = ws.id_sparepart where ws.id_woc = @id_woc)", con);
            cmd.Parameters.AddWithValue("@id_woc", id_woc);
            cmd.Parameters.AddWithValue("@status", 1);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                spareparts.Add(new SparepartModel()
                {
                    id = dr["id"].ToString(),
                    name = dr["name"].ToString(),
                    function = dr["function"].ToString(),
                    Stock = dr["stock"].ToString(),
                    unit = dr["unit"].ToString(),
                    price = dr["price"].ToString(),
                    status = dr["status"].ToString(),
                });
            };
            dr.Close();
            con.Close();
            return spareparts;
        }

    }
}