using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace _4S.DAL
{
    public class home
    {
        public int GetUserNumber()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select count(*) from T_Base_User";
            cm.Connection = co;

            Object count = cm.ExecuteScalar();
            co.Close();
            return (Int32)count - 1;
        }

        public int GetSalesOrderNumber()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select count(*) from T_Base_SalesOrder";
            cm.Connection = co;

            SqlCommand cn = new SqlCommand();
            cn.CommandText = "select count(*) from T_Base_CarPartSales";
            cn.Connection = co;

            int count = Convert.ToInt32(cm.ExecuteScalar()) + Convert.ToInt32(cn.ExecuteScalar());
            co.Close();
            return count;
        }

        public int GetRepairOrderNumber()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select count(*) from T_Base_Service";
            cm.Connection = co;

            Object count = cm.ExecuteScalar();
            co.Close();
            return (Int32)count;
        }

        public decimal GetTotalSales()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select sum(Price) from T_Base_Service";
            cm.Connection = co;

            SqlCommand cn = new SqlCommand();
            cn.CommandText = "select sum(TotalPrice) from T_Base_SalesOrder";
            cn.Connection = co;

            SqlCommand cd = new SqlCommand();
            cd.CommandText = "select sum(TotalPrice) from T_Base_CarPartSales";
            cd.Connection = co;

            decimal totalprice = Convert.ToDecimal(cm.ExecuteScalar()) + Convert.ToDecimal(cn.ExecuteScalar()) * 10000 + Convert.ToDecimal(cd.ExecuteScalar());

            return totalprice;
        }

        public Model.home GetModel()
        {
            Model.home model = new Model.home();
            model.UserNumber = GetUserNumber();
            model.SalesOrderNumber = GetSalesOrderNumber();
            model.RepairOrderNumber = GetRepairOrderNumber();
            model.TotalSales = GetTotalSales();
            return model;
        }

        public List<Model.T_Base_Employee> GetAllList()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select * from T_Base_Employee ";
            cm.Connection = co;

            List<Model.T_Base_Employee> lst = new List<Model.T_Base_Employee>();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                Model.T_Base_Employee user = new Model.T_Base_Employee();
                user.Id = Convert.ToInt32(dr["Id"]);
                user.Office = Convert.ToString(dr["Office"]);
                user.EntryDate = Convert.ToDateTime(dr["EntryDate"]);
                user.Email = Convert.ToString(dr["Email"]);
                user.PhoneNumber = Convert.ToString(dr["PhoneNumber"]);
                user.RealName = Convert.ToString(dr["RealName"]);
                lst.Add(user);
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
            cm.CommandText = "delete from T_Base_Employee where id=@id";
            cm.Parameters.AddWithValue("@id", id);
            cm.Connection = co;

            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public int Add(Model.T_Base_Employee model)
        {
            //把数据存入数据库
            //ado.net插入数据库
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "insert into T_Base_Employee (RealName,PhoneNumber,Email,Office,EntryDate) values (@RealName,@PhoneNumber,@Email, @Office,@EntryDate)";
            cm.Parameters.AddWithValue("@RealName", model.RealName);
            cm.Parameters.AddWithValue("@Email", model.Email);
            cm.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
            cm.Parameters.AddWithValue("@Office", model.Office);
            cm.Parameters.AddWithValue("@EntryDate", model.EntryDate);
            cm.Connection = co;
            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public Model.T_Base_Employee GetModel(int id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select * from T_Base_Employee where id=@id";
            cm.Parameters.AddWithValue("@id", id);

            cm.Connection = co;
            SqlDataReader dr = cm.ExecuteReader();
            Model.T_Base_Employee model = null;
            while (dr.Read())
            {
                model = new Model.T_Base_Employee();

                model.Id = Convert.ToInt32(dr["Id"]);
                model.RealName = Convert.ToString(dr["RealName"]);
                model.Email = Convert.ToString(dr["Email"]);
                model.PhoneNumber = Convert.ToString(dr["PhoneNumber"]);
                model.Office = Convert.ToString(dr["Office"]);
                model.EntryDate = Convert.ToDateTime(dr["EntryDate"]);
            }
            dr.Close();
            co.Close();
            return model;
        }

        public int Update(Model.T_Base_Employee model)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "update T_Base_Employee set RealName=@RealName,PhoneNumber=@PhoneNumber,Email=@Email,Office=@Office,EntryDate=@EntryDate where Id=@Id";
            cm.Parameters.AddWithValue("@RealName", model.RealName);
            cm.Parameters.AddWithValue("@Email", model.Email);
            cm.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
            cm.Parameters.AddWithValue("@Office", model.Office);
            cm.Parameters.AddWithValue("@EntryDate", model.EntryDate);
            cm.Parameters.AddWithValue("@Id", model.Id);
            cm.Connection = co;

            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public int SignInCheck(string LoginName, string password)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select PWD,Id from t_base_user where LoginName = '" + LoginName + "'";
            cm.Connection = co;
            string pwd = "";
            SqlDataReader dr = cm.ExecuteReader();
            int id = -1;
            while (dr.Read())
            {
                pwd = Convert.ToString(dr["PWD"]);
                id = Convert.ToInt32(dr["Id"]);
            }
            dr.Close();
            co.Close();
            if (pwd == password)
            {
                return id;
            }
            else
                return 0;
        }

        public int LoginCheck(string username, string password)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select PWD,Type from t_base_user where LoginName = '" + username + "'";
            cm.Connection = co;
            string pwd = "";
            int type = -1;
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                pwd = Convert.ToString(dr["PWD"]);
                type = Convert.ToInt32(dr["Type"]);
            }
            dr.Close();
            co.Close();
            if (pwd == password && type == 0)
            {
                return 1;
            }
            else
                return 0;
        }

        public List<Model.T_Base_Car> GetSomeCars()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select top(6) * from T_Base_Car";
            cm.Connection = co;
            SqlDataReader dr = cm.ExecuteReader();
            List<Model.T_Base_Car> lst = new List<Model.T_Base_Car>();
            while (dr.Read())
            {
                Model.T_Base_Car model = new Model.T_Base_Car();
                model.Id = Convert.ToInt32(dr["Id"]);
                model.Brand = Convert.ToString(dr["Brand"]);
                model.Carmodel = Convert.ToString(dr["Carmodel"]);
                model.Modeldetail = Convert.ToString(dr["Modeldetail"]);
                model.Price = Convert.ToDecimal(dr["Price"]);
                model.Stock = Convert.ToInt32(dr["Stock"]);
                model.Color = Convert.ToInt32(dr["Color"]);
                model.Image = Convert.ToString(dr["Image"]);
                lst.Add(model);
            }

            dr.Close();
            co.Close();
            return lst;
        }


        public List<Model.T_Base_Car> GetCars(string carmodel)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select  * from T_Base_Car where Carmodel = '" + carmodel + "'";
            cm.Connection = co;
            SqlDataReader dr = cm.ExecuteReader();
            List<Model.T_Base_Car> lst = new List<Model.T_Base_Car>();
            while (dr.Read())
            {
                Model.T_Base_Car model = new Model.T_Base_Car();
                model.Id = Convert.ToInt32(dr["Id"]);
                model.Brand = Convert.ToString(dr["Brand"]);
                model.Carmodel = Convert.ToString(dr["Carmodel"]);
                model.Modeldetail = Convert.ToString(dr["Modeldetail"]);
                model.Price = Convert.ToDecimal(dr["Price"]);
                model.Stock = Convert.ToInt32(dr["Stock"]);
                model.Color = Convert.ToInt32(dr["Color"]);
                model.Image = Convert.ToString(dr["Image"]);
                lst.Add(model);
            }

            dr.Close();
            co.Close();
            return lst;
        }

        public Model.T_Base_Car GetCarDetail(int id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();           
            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select * from V_Base_Carmsg where id=@id";
            cm.Parameters.AddWithValue("@id", id);

            cm.Connection = co;
            SqlDataReader dr = cm.ExecuteReader();
            Model.T_Base_Car model = null;
            while (dr.Read())
            {
                model = new Model.T_Base_Car();

                model.Id = Convert.ToInt32(dr["Id"]);
                model.Brand = Convert.ToString(dr["Brand"]);
                model.Carmodel = Convert.ToString(dr["Carmodel"]);
                model.Modeldetail = Convert.ToString(dr["Modeldetail"]);
                model.Price = Convert.ToDecimal(dr["Price"]);
                model.Stock = Convert.ToInt32(dr["Stock"]);
                model.Color = Convert.ToInt32(dr["Color"]);
                model.Image = Convert.ToString(dr["Image"]);
                model.BasicparameterId = Convert.ToInt32(dr["BasicparameterId"]);

                Model.T_Base_CarParameter Par = new Model.T_Base_CarParameter();
                Par.Id = Convert.ToInt32(dr["BasicparameterId"]);
                Par.Level = Convert.ToString(dr["Level"]);
                Par.Engine = Convert.ToString(dr["Engine"]);
                Par.Gearbox = Convert.ToString(dr["Gearbox"]);
                Par.Powertype = Convert.ToString(dr["Powertype"]);
                Par.LWG = Convert.ToString(dr["LWG"]);
                Par.Bodystructure = Convert.ToString(dr["Bodystructure"]);
                Par.Listingyear = Convert.ToString(dr["Listingyear"]);
                Par.Topspeed = Convert.ToInt32(dr["Topspeed"]);
                Par.accelerate = Convert.ToDecimal(dr["accelerate"]);
                model.CarParameter = Par;
            }
            dr.Close();
            co.Close();
            return model;
        }

        public Model.T_Base_User PersonCenter(int id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select * from T_Base_User where id=@id";
            cm.Parameters.AddWithValue("@id", id);

            cm.Connection = co;
            SqlDataReader dr = cm.ExecuteReader();
            Model.T_Base_User model = null;
            while (dr.Read())
            {
                model = new Model.T_Base_User();

                model.Id = Convert.ToInt32(dr["Id"]);
                model.LoginName = Convert.ToString(dr["LoginName"]);
                model.PWD = Convert.ToString(dr["PWD"]);
                model.RealName = Convert.ToString(dr["RealName"]);
                model.Email = Convert.ToString(dr["Email"]);
                model.Phonenumber = Convert.ToString(dr["Phonenumber"]);
                model.Type = Convert.ToInt32(dr["Type"]);
                model.Remark = Convert.ToString(dr["Remark"]);
            }
            dr.Close();
            co.Close();
            return model;
        }

        public List<Model.T_Base_Service> GetPersonService(int id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select * from V_Base_ServiceMsg where Userid=@id";
            cm.Parameters.AddWithValue("@id", id);

            cm.Connection = co;
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
                lst.Add(model);
            }
            dr.Close();
            co.Close();
            return lst;
        }

        public List<Model.T_Base_Testdrive> GetPersonReserve(int id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select * from V_Base_Testdrive where Userid=@id";
            cm.Parameters.AddWithValue("@id", id);

            cm.Connection = co;
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
                car.Id = Convert.ToInt32(dr["CarId"]);
                car.Carmodel = Convert.ToString(dr["Carmodel"]);
                car.Modeldetail = Convert.ToString(dr["Modeldetail"]);
                model.Car = car;
                lst.Add(model);
            }
            dr.Close();
            co.Close();
            return lst;
        }

        public List<Model.T_Base_CarPart> GetAllCarPart()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select  * from T_Base_CarPart ";
            cm.Connection = co;
            SqlDataReader dr = cm.ExecuteReader();
            List<Model.T_Base_CarPart> lst = new List<Model.T_Base_CarPart>();
            while (dr.Read())
            {
                Model.T_Base_CarPart carPart = new Model.T_Base_CarPart();
                carPart.Brand = Convert.ToString(dr["Brand"]);
                carPart.Id = Convert.ToInt32(dr["Id"]);
                carPart.Name = Convert.ToString(dr["Name"]);
                carPart.Price = Convert.ToDecimal(dr["Price"]);
                carPart.Applicable = Convert.ToString(dr["Applicable"]);
                carPart.Note = Convert.ToString(dr["Note"]);
                carPart.Picture = Convert.ToString(dr["Picture"]);
                carPart.Stock = Convert.ToInt32(dr["Stock"]);
                lst.Add(carPart);
            }
            return lst;
        }

        public Model.T_Base_CarPart GetCarPartDetail(int id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select  * from T_Base_CarPart where @id=id";
            cm.Parameters.AddWithValue("@id", id);
            cm.Connection = co;
            SqlDataReader dr = cm.ExecuteReader();
            Model.T_Base_CarPart model = new Model.T_Base_CarPart();
            while (dr.Read())
            {
                model.Brand = Convert.ToString(dr["Brand"]);
                model.Id = Convert.ToInt32(dr["Id"]);
                model.Name = Convert.ToString(dr["Name"]);
                model.Price = Convert.ToDecimal(dr["Price"]);
                model.Applicable = Convert.ToString(dr["Applicable"]);
                model.Note = Convert.ToString(dr["Note"]);
                model.Picture = Convert.ToString(dr["Picture"]);
                model.Stock = Convert.ToInt32(dr["Stock"]);
            }
            return model;
        }
    }
}
