using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace _4S.DAL
{
    public class T_Base_Testdrive
    {
        public int GetCount()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select count(*) from V_Base_Testdrive";
            cm.Connection = co;

            Object count = cm.ExecuteScalar();
            co.Close();
            return (Int32)count;
        }

        public List<Model.T_Base_Testdrive> GetlistByPage(int pageSize, int pageNumber, string search, string sortName, string sortOrder, string RealName, string Carmodel)
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
                string subtable = "(select top " + skipcount + " Id from V_Base_Testdrive where " + where + " " + order + "  )";
                cm.CommandText = "select top " + pageSize + " * from V_Base_Testdrive where  " + where + " and id not in " + subtable + " " + order;
            }
            else
            {
                string temp_table = "(select * from V_Base_Testdrive where " + "RealName like '%" + search + "%' or  Carmodel like '%" + search + "%') as temp_table";
                string subtable = "(select top " + skipcount + " Id from " + temp_table + " where " + where + " " + order + "  )";
                cm.CommandText = "select top " + pageSize + " * from " + temp_table + "  where  " + where + "and id not in " + subtable + " " + order;
            }

            SqlDataReader dr = cm.ExecuteReader();
            List<Model.T_Base_Testdrive> lst = new List<Model.T_Base_Testdrive>();
            while (dr.Read())
            {
                Model.T_Base_Testdrive model = new Model.T_Base_Testdrive();
                model.Id = Convert.ToInt32(dr["Id"]);
                model.Dtime = Convert.ToDateTime(dr["Dtime"]);
                model.DealSituation = Convert.ToInt32(dr["DealSituation"]);
                model.DealPerson = Convert.ToString(dr["DealPerson"]);
                model.CarId = Convert.ToInt32(dr["CarId"]);
                model.UserId = Convert.ToInt32(dr["UserId"]);
                Model.T_Base_Car car = new Model.T_Base_Car();
                car.Id= Convert.ToInt32(dr["CarId"]);
                car.Carmodel= Convert.ToString(dr["Carmodel"]);
                car.Modeldetail = Convert.ToString(dr["Modeldetail"]);
                model.Car = car;

                Model.T_Base_User user = new Model.T_Base_User();
                user.Id = Convert.ToInt32(dr["UserId"]);
                user.RealName = Convert.ToString(dr["RealName"]);
                user.Phonenumber = Convert.ToString(dr["Phonenumber"]);
                model.User = user;
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
            cm.CommandText = "delete from T_Base_Testdrive where id=@id";
            cm.Parameters.AddWithValue("@id", id);
            cm.Connection = co;

            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public int Deletes(string ids)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "delete from T_Base_Testdrive where id  in (" + ids + ")";
            cm.Connection = co;
            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public int Add(Model.T_Base_Testdrive model)
        {
            //把数据存入数据库
            //ado.net插入数据库
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "insert into T_Base_Testdrive (CarId,UserId,Dtime,DealSituation,DealPerson) values (@CarId, @UserId, @Dtime, @DealSituation, @DealPerson)";
            cm.Parameters.AddWithValue("@CarId", model.CarId);
            cm.Parameters.AddWithValue("@UserId", model.UserId);
            cm.Parameters.AddWithValue("@Dtime", model.Dtime);
            cm.Parameters.AddWithValue("@DealSituation", model.DealSituation);              
            cm.Parameters.AddWithValue("@DealPerson", model.DealPerson);
            cm.Connection = co;
            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public Model.T_Base_Testdrive GetModel(int id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select * from T_Base_Testdrive where id=@id";
            cm.Parameters.AddWithValue("@id", id);

            cm.Connection = co;
            SqlDataReader dr = cm.ExecuteReader();
            Model.T_Base_Testdrive model = null;
            while (dr.Read())
            {
                model = new Model.T_Base_Testdrive();

                model.Id = Convert.ToInt32(dr["Id"]);
                model.CarId = Convert.ToInt32(dr["CarId"]);        
                model.Dtime = Convert.ToDateTime(dr["Dtime"]);
                model.DealSituation = Convert.ToInt32(dr["DealSituation"]);
                model.DealPerson = Convert.ToString(dr["DealPerson"]);
                model.UserId = Convert.ToInt32(dr["UserId"]);

            }
            dr.Close();
            co.Close();
            return model;
        }

        public int Update(Model.T_Base_Testdrive model)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "update T_Base_Testdrive set CarId=@CarId,UserId=@UserId,Dtime=@Dtime,DealSituation=@DealSituation,DealPerson=@DealPerson where Id=@Id";
            cm.Parameters.AddWithValue("@CarId", model.CarId);
            cm.Parameters.AddWithValue("@UserId", model.UserId);
            cm.Parameters.AddWithValue("@Dtime", model.Dtime);
            cm.Parameters.AddWithValue("@DealSituation", model.DealSituation);
            cm.Parameters.AddWithValue("@DealPerson", model.DealPerson);
            cm.Parameters.AddWithValue("@Id", model.Id);
            cm.Connection = co;

            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }
    }
}
