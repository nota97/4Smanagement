using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace _4S.DAL
{
    public class T_Base_Service
    {
        public int GetCount()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select count(*) from V_Base_ServiceMsg";
            cm.Connection = co;

            Object count = cm.ExecuteScalar();
            co.Close();
            return (Int32)count;
        }

        public List<Model.T_Base_Service> GetlistByPage(int pageSize, int pageNumber, string search, string sortName, string sortOrder, string RealName, string Carmodel)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            int skipcount = (pageNumber - 1) * pageSize;
            string order = " order by " + sortName + " " + sortOrder + " ";

            string where = "  (Carmodel like '%" + Carmodel + "%' and  RealName like '%" + RealName + "%' ) ";
            if (search == "")
            {
                string subtable = "(select top " + skipcount + " Id from V_Base_ServiceMsg where " + where + " " + order + "  )";
                cm.CommandText = "select top " + pageSize + " * from V_Base_ServiceMsg where  " + where + " and id not in " + subtable + " " + order;
            }
            else
            {
                string temp_table = "(select * from V_Base_ServiceMsg where " + "RealName like '%" + search + "%' or  Carmodel like '%" + search + "%') as temp_table";
                string subtable = "(select top " + skipcount + " Id from " + temp_table + " where " + where + " " + order + "  )";
                cm.CommandText = "select top " + pageSize + " * from " + temp_table + "  where  " + where + "and id not in " + subtable + " " + order;
            }

            SqlDataReader dr = cm.ExecuteReader();
            List<Model.T_Base_Service> lst = new List<Model.T_Base_Service>();
            while (dr.Read())
            {
                Model.T_Base_Service model = new Model.T_Base_Service();
                model.Id = Convert.ToInt32(dr["Id"]);
                model.Brand = Convert.ToString(dr["Brand"]);
                model.CarModel = Convert.ToString(dr["CarModel"]);
                model.Modeldetail = Convert.ToString(dr["Modeldetail"]);
                model.FaultType = Convert.ToString(dr["FaultType"]);
                model.Appointment = Convert.ToDateTime(dr["Appointment"]);
                model.DealSituation = Convert.ToInt32(dr["DealSituation"]);
                model.Note = Convert.ToString(dr["Note"]);
                model.UserId = Convert.ToInt32(dr["UserId"]);
                if (Convert.IsDBNull(dr["Price"]))
                {
                    model.Price = 0;
                }
                else
                    model.Price = Convert.ToDecimal(dr["Price"]);

                Model.T_Base_User user = new Model.T_Base_User();
                user.Id = Convert.ToInt32(dr["UserId"]);
                user.RealName = Convert.ToString(dr["RealName"]);
                user.Phonenumber = Convert.ToString(dr["Phonenumber"]);
                model.user = user;
                lst.Add(model);
            }
            dr.Close();
            co.Close();
            return lst;
        }

        public int Delete(int id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "delete from T_Base_Service where id=@id";
            cm.Parameters.AddWithValue("@id", id);
            cm.Connection = co;

            int result = cm.ExecuteNonQuery() ;
            co.Close();
            return result;
        }

        public int Deletes(string ids)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "delete from T_Base_Service where id  in (" + ids + ")";
            cm.Connection = co;
            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public int Add(Model.T_Base_Service model)
        {
            //把数据存入数据库
            //ado.net插入数据库
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open(); 

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "insert into T_Base_Service (Brand,CarModel,Modeldetail,Price,FaultType,Appointment,DealSituation,Note,UserId) values (@Brand, @CarModel, @Modeldetail, @Price, @FaultType,@Appointment,@DealSituation,@Note,@UserId)";
            cm.Parameters.AddWithValue("@Brand", model.Brand);
            cm.Parameters.AddWithValue("@CarModel", model.CarModel);
            cm.Parameters.AddWithValue("@Modeldetail", model.Modeldetail);
            cm.Parameters.AddWithValue("@Price", model.Price);
            cm.Parameters.AddWithValue("@FaultType", model.FaultType);
            cm.Parameters.AddWithValue("@Appointment", model.Appointment);
            cm.Parameters.AddWithValue("@DealSituation", model.DealSituation);
            cm.Parameters.AddWithValue("@Note", model.Note);
            cm.Parameters.AddWithValue("@UserId", model.UserId);
            cm.Connection = co;
            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public Model.T_Base_Service GetModel(int id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select * from T_Base_Service where id=@id";
            cm.Parameters.AddWithValue("@id", id);

            cm.Connection = co;
            SqlDataReader dr = cm.ExecuteReader();
            Model.T_Base_Service model = null;
            while (dr.Read())
            {
                model = new Model.T_Base_Service();

                model.Id = Convert.ToInt32(dr["Id"]);
                model.Brand = Convert.ToString(dr["Brand"]);
                model.CarModel = Convert.ToString(dr["CarModel"]);
                model.Modeldetail = Convert.ToString(dr["Modeldetail"]);
                model.Price = Convert.ToDecimal(dr["Price"]);
                model.FaultType = Convert.ToString(dr["FaultType"]);
                model.Appointment = Convert.ToDateTime(dr["Appointment"]);
                model.DealSituation = Convert.ToInt32(dr["DealSituation"]);
                model.Note = Convert.ToString(dr["Note"]);
                model.UserId = Convert.ToInt32(dr["UserId"]);
 
            }
            dr.Close();
            co.Close();
            return model;
        }

        public int Update(Model.T_Base_Service model)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();
            SqlCommand cn = new SqlCommand();   

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "update T_Base_Service set Brand=@Brand,CarModel=@CarModel,Modeldetail=@Modeldetail,Price=@Price,FaultType=@FaultType,Appointment=@Appointment,DealSituation=@DealSituation,Note=@Note,UserId=@UserId where Id=@Id";
            cm.Parameters.AddWithValue("@Brand", model.Brand);
            cm.Parameters.AddWithValue("@CarModel", model.CarModel);
            cm.Parameters.AddWithValue("@Modeldetail", model.Modeldetail);
            cm.Parameters.AddWithValue("@Price", model.Price);
            cm.Parameters.AddWithValue("@FaultType", model.FaultType);
            cm.Parameters.AddWithValue("@Appointment", model.Appointment);
            cm.Parameters.AddWithValue("@DealSituation", model.DealSituation);
            cm.Parameters.AddWithValue("@Note", model.Note);
            cm.Parameters.AddWithValue("@UserId", model.UserId);
            cm.Parameters.AddWithValue("@Id", model.Id);
            cm.Connection = co;

            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }
    }
}
