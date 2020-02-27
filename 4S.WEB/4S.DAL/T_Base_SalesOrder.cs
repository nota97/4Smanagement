using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace _4S.DAL
{
    public class T_Base_SalesOrder
    {
        public int GetCount()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select count(*) from V_Base_SalesOrder";
            cm.Connection = co;

            Object count = cm.ExecuteScalar();
            co.Close();
            return (Int32)count;
        }

        public List<Model.T_Base_SalesOrder> GetlistByPage(int pageSize, int pageNumber, string search, string sortName, string sortOrder, string RealName, string Carmodel)
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
                string subtable = "(select top " + skipcount + " Id from V_Base_SalesOrder where " + where + " " + order + "  )";
                cm.CommandText = "select top " + pageSize + " * from V_Base_SalesOrder where  " + where + " and id not in " + subtable + " " + order;
            }
            else
            {
                string temp_table = "(select * from V_Base_SalesOrder where " + "RealName like '%" + search + "%' or  Carmodel like '%" + search + "%') as temp_table";
                string subtable = "(select top " + skipcount + " Id from " + temp_table + " where " + where + " " + order + "  )";
                cm.CommandText = "select top " + pageSize + " * from " + temp_table + "  where  " + where + "and id not in " + subtable + " " + order;
            }

            SqlDataReader dr = cm.ExecuteReader();
            List<Model.T_Base_SalesOrder> lst = new List<Model.T_Base_SalesOrder>();
            while (dr.Read())
            {
                Model.T_Base_SalesOrder model = new Model.T_Base_SalesOrder();
                model.Id = Convert.ToInt32(dr["Id"]);
                model.Dtime = Convert.ToDateTime(dr["Dtime"]);
                model.Amount = Convert.ToInt32(dr["Amount"]);
                model.TotalPrice = Convert.ToDecimal(dr["TotalPrice"]);
                model.CarId = Convert.ToInt32(dr["CarId"]);
                model.UserId = Convert.ToInt32(dr["UserId"]);
                model.Seller=Convert.ToString(dr["Seller"]);
                Model.T_Base_Car car = new Model.T_Base_Car();
                car.Id = Convert.ToInt32(dr["CarId"]);
                car.Carmodel = Convert.ToString(dr["Carmodel"]);
                car.Modeldetail = Convert.ToString(dr["Modeldetail"]);
                car.Price= Convert.ToDecimal(dr["Price"]);
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

        public int GetCarId(string id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select CarId from T_Base_SalesOrder where id="+id;
            cm.Connection = co;

            Object count = cm.ExecuteScalar();
            co.Close();
            return (Int32)count;
        }

        public int GetAmount(string id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select Amount from T_Base_SalesOrder where id=" + id;
            cm.Connection = co;

            Object count = cm.ExecuteScalar();
            co.Close();
            return (Int32)count;
        }

        public int GetUserId(string id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select UserId from T_Base_SalesOrder where id=" + id;
            cm.Connection = co;

            Object count = cm.ExecuteScalar();
            co.Close();
            return (Int32)count;
        }

        public int GetUserAmount(string id)
        {

            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select count(*) from T_Base_SalesOrder where UserId=" + id;
            cm.Connection = co;

            Object count = cm.ExecuteScalar();
            co.Close();
            return (Int32)count;
        }

        public int Delete(int id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();
            int amount = GetAmount(id.ToString());
            int UserId = GetUserId(id.ToString());
            int flag = 0;
            if (GetUserAmount(UserId.ToString()) < 2)
            {
                SqlCommand cd = new SqlCommand();
                cd.CommandText = "update T_Base_User set Type=1 where Id=" + UserId.ToString();
                cd.Connection = co;
                flag = 1;
            }

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "delete from T_Base_SalesOrder where id=@id";
            cm.Parameters.AddWithValue("@id", id);
            cm.Connection = co;

            int carId = GetCarId(id.ToString());
            SqlCommand cn = new SqlCommand();
            cn.CommandText = "update T_Base_Car set Stock=Stock+"+amount.ToString()+" where Id="+carId.ToString();
            cn.Connection = co;
         
            int result = cm.ExecuteNonQuery()+ cn.ExecuteNonQuery()+flag ;
            co.Close();
            return result;
        }

        public int Deletes(string ids)
        {
            //SqlConnection co = new SqlConnection();
            //co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            //co.Open();
            string[] lst = ids.Split(',');
            int result = 0;
            for(int i = 0; i < lst.Length; i++)
            {
                int id = Convert.ToInt32(lst[i]);
                result=result+Delete(id);
            }
            //SqlCommand cm = new SqlCommand();
            //cm.CommandText = "delete from T_Base_SalesOrder where id  in (" + ids + ")";
            //cm.Connection = co;
            //int result = cm.ExecuteNonQuery();
            //co.Close();
            return result;
        }

        public int GetStock(string id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select Stock from T_Base_Car where id=" + id;
            cm.Connection = co;

            Object count = cm.ExecuteScalar();
            co.Close();
            return (Int32)count;
        }

        public decimal GetPrice(string id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select Price from T_Base_Car where id=" + id;
            cm.Connection = co;

            Object count = cm.ExecuteScalar();
            decimal price = Convert.ToDecimal(count);
            co.Close();
            return price;
        }

        public int Add(Model.T_Base_SalesOrder model)
        {
            //把数据存入数据库
            //ado.net插入数据库
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();
            int amount = model.Amount;
            int carId = model.CarId;
            model.TotalPrice = amount * GetPrice(carId.ToString());
            if (GetStock(carId.ToString())<amount)
            {
                return -1;
            }
            SqlCommand cm = new SqlCommand();
            cm.CommandText = "insert into T_Base_SalesOrder (CarId,Amount,TotalPrice,Dtime,UserId,Seller) values (@CarId, @Amount, @TotalPrice, @Dtime, @UserId,@Seller)";
            cm.Parameters.AddWithValue("@CarId", model.CarId);
            cm.Parameters.AddWithValue("@Amount", model.Amount);
            cm.Parameters.AddWithValue("@TotalPrice", model.TotalPrice);
            cm.Parameters.AddWithValue("@Dtime", model.Dtime);
            cm.Parameters.AddWithValue("@UserId", model.UserId);
            cm.Parameters.AddWithValue("@Seller", model.Seller);
            cm.Connection = co;
            
            SqlCommand cn = new SqlCommand();
            cn.CommandText = "update T_Base_Car set Stock=Stock-" + amount.ToString() + " where Id=" + carId.ToString();
            cn.Connection = co;

            SqlCommand cd = new SqlCommand();
            cd.CommandText = "update T_Base_User set Type=2 where Id=" + model.UserId.ToString();
            cd.Connection = co;

            int result = cm.ExecuteNonQuery()+cn.ExecuteNonQuery()+cd.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public Model.T_Base_SalesOrder GetModel(int id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select * from T_Base_SalesOrder where id=@id";
            cm.Parameters.AddWithValue("@id", id);

            cm.Connection = co;
            SqlDataReader dr = cm.ExecuteReader();
            Model.T_Base_SalesOrder model = null;
            while (dr.Read())
            {
                model = new Model.T_Base_SalesOrder();
                model.Id = Convert.ToInt32(dr["Id"]);
                model.CarId = Convert.ToInt32(dr["CarId"]);
                model.Dtime = Convert.ToDateTime(dr["Dtime"]);
                model.Amount = Convert.ToInt32(dr["Amount"]);
                model.Seller = Convert.ToString(dr["Seller"]);
                model.UserId = Convert.ToInt32(dr["UserId"]);
            }
            dr.Close();
            co.Close();
            return model;
        }

        public int Update(Model.T_Base_SalesOrder model)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();
            int oldamount = GetAmount(model.Id.ToString());
            int amount = model.Amount;
            int carId = model.CarId;
            model.TotalPrice = amount * GetPrice(carId.ToString());
            if (GetStock(carId.ToString())+oldamount < amount)
            {
                return -1;
            }
            SqlCommand cm = new SqlCommand();
            cm.CommandText = "update T_Base_SalesOrder set CarId=@CarId,UserId=@UserId,Dtime=@Dtime,Amount=@Amount,TotalPrice=@TotalPrice,Seller=@Seller where Id=@Id";
            cm.Parameters.AddWithValue("@CarId", model.CarId);
            cm.Parameters.AddWithValue("@UserId", model.UserId);
            cm.Parameters.AddWithValue("@Dtime", model.Dtime);
            cm.Parameters.AddWithValue("@Amount", model.Amount);
            cm.Parameters.AddWithValue("@Seller", model.Seller);
            cm.Parameters.AddWithValue("@TotalPrice", model.TotalPrice);
            cm.Parameters.AddWithValue("@Id", model.Id);
            cm.Connection = co;

            SqlCommand cn = new SqlCommand();
            cn.CommandText = "update T_Base_Car set Stock=Stock+"+oldamount.ToString()+"-" + amount.ToString() + " where Id=" + carId.ToString();
            cn.Connection = co;

            SqlCommand cd = new SqlCommand();
            cd.CommandText = "update T_Base_User set Type=2 where Id=" + model.UserId.ToString();
            cd.Connection = co;

            int result = cm.ExecuteNonQuery()+ cn.ExecuteNonQuery()+ cd.ExecuteNonQuery();
            co.Close();
            return result;
        }
    }
}
