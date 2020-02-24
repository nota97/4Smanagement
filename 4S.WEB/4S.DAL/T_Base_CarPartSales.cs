using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace _4S.DAL
{
    public class T_Base_CarPartSales
    {
        public int GetCount()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select count(*) from V_Base_CarPartSales";
            cm.Connection = co;

            Object count = cm.ExecuteScalar();
            co.Close();
            return (Int32)count;
        }

        public List<Model.T_Base_CarPartSales> GetlistByPage(int pageSize, int pageNumber, string search, string sortName, string sortOrder, string RealName, string Name)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            int skipcount = (pageNumber - 1) * pageSize;
            string order = " order by " + sortName + " " + sortOrder + " ";

            string where = "  (Name like '%" + Name + "%' and  RealName like '%" + RealName + "%' ) ";
            if (search == "")
            {
                string subtable = "(select top " + skipcount + " Id from V_Base_CarPartSales where " + where + " " + order + "  )";
                cm.CommandText = "select top " + pageSize + " * from V_Base_CarPartSales where  " + where + " and id not in " + subtable + " " + order;
            }
            else
            {
                string temp_table = "(select * from V_Base_CarPartSales where " + "RealName like '%" + search + "%' or  Name like '%" + search + "%') as temp_table";
                string subtable = "(select top " + skipcount + " Id from " + temp_table + " where " + where + " " + order + "  )";
                cm.CommandText = "select top " + pageSize + " * from " + temp_table + "  where  " + where + "and id not in " + subtable + " " + order;
            }

            SqlDataReader dr = cm.ExecuteReader();
            List<Model.T_Base_CarPartSales> lst = new List<Model.T_Base_CarPartSales>();
            while (dr.Read())
            {
                Model.T_Base_CarPartSales model = new Model.T_Base_CarPartSales();
                model.Id = Convert.ToInt32(dr["Id"]);
                model.Dtime = Convert.ToDateTime(dr["Dtime"]);
                model.Amount = Convert.ToInt32(dr["Amount"]);
                model.TotalPrice = Convert.ToDecimal(dr["TotalPrice"]);
                model.CarPartId = Convert.ToInt32(dr["CarPartId"]);
                model.UserId = Convert.ToInt32(dr["UserId"]);
                model.Seller = Convert.ToString(dr["Seller"]);
                Model.T_Base_CarPart car = new Model.T_Base_CarPart();
                car.Id = Convert.ToInt32(dr["CarPartId"]);
                car.Brand = Convert.ToString(dr["Brand"]);
                car.Name = Convert.ToString(dr["Name"]);
                car.Price = Convert.ToDecimal(dr["Price"]);
                model.CarPart = car;

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

        public int GetCarPartId(string id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select CarPartId from T_Base_CarPartSales where id=" + id;
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
            cm.CommandText = "select Amount from T_Base_CarPartSales where id=" + id;
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

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "delete from T_Base_CarPartSales where id=@id";
            cm.Parameters.AddWithValue("@id", id);
            cm.Connection = co;

            int carpartId = GetCarPartId(id.ToString());
            SqlCommand cn = new SqlCommand();
            cn.CommandText = "update T_Base_CarPart set Stock=Stock+" + amount.ToString() + " where Id=" + carpartId.ToString();
            cn.Connection = co;

            int result = cm.ExecuteNonQuery() + cn.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public int Deletes(string ids)
        {         
            string[] lst = ids.Split(',');
            int result = 0;
            for (int i = 0; i < lst.Length; i++)
            {
                int id = Convert.ToInt32(lst[i]);
                result = result + Delete(id);
            }
            return result;
        }

        public int GetStock(string id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select Stock from T_Base_CarPart where id=" + id;
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
            cm.CommandText = "select Price from T_Base_CarPart where id=" + id;
            cm.Connection = co;

            Object count = cm.ExecuteScalar();
            decimal price = Convert.ToDecimal(count);
            co.Close();
            return price;
        }

        public int Add(Model.T_Base_CarPartSales model)
        {
            //把数据存入数据库
            //ado.net插入数据库
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();
            int amount = model.Amount;
            int carpartId = model.CarPartId;
            model.TotalPrice = amount * GetPrice(carpartId.ToString());
            if (GetStock(carpartId.ToString()) < amount)
            {
                return -1;
            }
            SqlCommand cm = new SqlCommand();
            cm.CommandText = "insert into T_Base_CarPartSales (CarPartId,Amount,TotalPrice,Dtime,UserId,Seller) values (@CarPartId, @Amount, @TotalPrice, @Dtime, @UserId,@Seller)";
            cm.Parameters.AddWithValue("@CarPartId", model.CarPartId);
            cm.Parameters.AddWithValue("@Amount", model.Amount);
            cm.Parameters.AddWithValue("@TotalPrice", model.TotalPrice);
            cm.Parameters.AddWithValue("@Dtime", model.Dtime);
            cm.Parameters.AddWithValue("@UserId", model.UserId);
            cm.Parameters.AddWithValue("@Seller", model.Seller);
            cm.Connection = co;

            SqlCommand cn = new SqlCommand();
            cn.CommandText = "update T_Base_CarPart set Stock=Stock-" + amount.ToString() + " where Id=" + carpartId.ToString();
            cn.Connection = co;

            int result = cm.ExecuteNonQuery() + cn.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public Model.T_Base_CarPartSales GetModel(int id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select * from T_Base_CarPartSales where id=@id";
            cm.Parameters.AddWithValue("@id", id);

            cm.Connection = co;
            SqlDataReader dr = cm.ExecuteReader();
            Model.T_Base_CarPartSales model = null;
            while (dr.Read())
            {
                model = new Model.T_Base_CarPartSales();
                model.Id = Convert.ToInt32(dr["Id"]);
                model.CarPartId = Convert.ToInt32(dr["CarPartId"]);
                model.Dtime = Convert.ToDateTime(dr["Dtime"]);
                model.Amount = Convert.ToInt32(dr["Amount"]);
                model.Seller = Convert.ToString(dr["Seller"]);
                model.UserId = Convert.ToInt32(dr["UserId"]);
            }
            dr.Close();
            co.Close();
            return model;
        }

        public int Update(Model.T_Base_CarPartSales model)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();
            int oldamount = GetAmount(model.Id.ToString());
            int amount = model.Amount;
            int carpartId = model.CarPartId;
            model.TotalPrice = amount * GetPrice(carpartId.ToString());
            if (GetStock(carpartId.ToString()) + oldamount < amount)
            {
                return -1;
            }
            SqlCommand cm = new SqlCommand();
            cm.CommandText = "update T_Base_CarPartSales set CarPartId=@CarPartId,UserId=@UserId,Dtime=@Dtime,Amount=@Amount,TotalPrice=@TotalPrice,Seller=@Seller where Id=@Id";
            cm.Parameters.AddWithValue("@CarPartId", model.CarPartId);
            cm.Parameters.AddWithValue("@UserId", model.UserId);
            cm.Parameters.AddWithValue("@Dtime", model.Dtime);
            cm.Parameters.AddWithValue("@Amount", model.Amount);
            cm.Parameters.AddWithValue("@Seller", model.Seller);
            cm.Parameters.AddWithValue("@TotalPrice", model.TotalPrice);
            cm.Parameters.AddWithValue("@Id", model.Id);
            cm.Connection = co;

            SqlCommand cn = new SqlCommand();
            cn.CommandText = "update T_Base_CarPart set Stock=Stock+" + oldamount.ToString() + "-" + amount.ToString() + " where Id=" + carpartId.ToString();
            cn.Connection = co;

            int result = cm.ExecuteNonQuery() + cn.ExecuteNonQuery();
            co.Close();
            return result;
        }
    }
}
