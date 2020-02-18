using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace _4S.DAL
{
    public class T_Base_Car
    {
        public int GetCount()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select count(*) from V_Base_Carmsg";
            cm.Connection = co;

            Object count = cm.ExecuteScalar();
            co.Close();
            return (Int32)count;
        }

        public List<Model.T_Base_Car> GetlistByPage(int pageSize, int pageNumber, string search, string sortName, string sortOrder, string Brand, string Carmodel)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            int skipcount = (pageNumber - 1) * pageSize;
            string order = " order by " + sortName + " " + sortOrder + " ";

            string where = "  (Carmodel like '%" + Carmodel + "%' and  Brand like '%" + Brand + "%' ) ";
            if (search == "")
            {
                string subtable = "(select top " + skipcount + " Id from V_Base_Carmsg where " + where + " " + order + "  )";
                cm.CommandText = "select top " + pageSize + " * from V_Base_Carmsg where  " + where + " and id not in " + subtable + " " + order;
            }
            else
            {
                string temp_table = "(select * from V_Base_Carmsg where " + "Brand like '%" + search + "%' or  Carmodel like '%" + search + "%') as temp_table";
                string subtable = "(select top " + skipcount + " Id from " + temp_table + " where " + where + " " + order + "  )";
                cm.CommandText = "select top " + pageSize + " * from " + temp_table + "  where  " + where + "and id not in " + subtable + " " + order;
            }

            SqlDataReader dr = cm.ExecuteReader();
            List<Model.T_Base_Car> lst = new List<Model.T_Base_Car>();
            while (dr.Read())
            {
                Model.T_Base_Car user = new Model.T_Base_Car();
                user.Id = Convert.ToInt32(dr["Id"]);
                user.Brand = Convert.ToString(dr["Brand"]);
                user.Carmodel = Convert.ToString(dr["Carmodel"]);
                user.Modeldetail = Convert.ToString(dr["Modeldetail"]);
                user.Price = Convert.ToDecimal(dr["Price"]);
                user.Stock = Convert.ToInt32(dr["Stock"]);
                user.Color = Convert.ToInt32(dr["Color"]);
                user.Image = Convert.ToString(dr["Image"]);
                if (Convert.IsDBNull(dr["BasicparameterId"])) {
                    user.BasicparameterId = 0; }
                else
                    user.BasicparameterId = Convert.ToInt32(dr["BasicparameterId"]);

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

                user.CarParameter = Par;               
                lst.Add(user);            
            }
            dr.Close();
            co.Close();
            return lst;
        }

        public int Delete(int id,int BasicparameterId)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "delete from T_Base_Car where id=@id";
            cm.Parameters.AddWithValue("@id", id);
            cm.Connection = co;

            SqlCommand cn = new SqlCommand();
            cn.CommandText = "delete from T_Base_CarParameter where id=@BasicparameterId";
            cn.Parameters.AddWithValue("@BasicparameterId", BasicparameterId);
            cn.Connection = co;
            int result = cm.ExecuteNonQuery()+ cn.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public int Deletes(string ids,string BasicparameterIds)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "delete from T_Base_Car where id   in (" + ids + ")";
            cm.Connection = co;

            SqlCommand cn = new SqlCommand();
            cn.CommandText = "delete from T_Base_CarParameter where id   in (" + BasicparameterIds + ")";
            cn.Connection = co;

            int result = cm.ExecuteNonQuery()+cn.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public int Add(Model.T_Base_Car model)
        {
            //把数据存入数据库
            //ado.net插入数据库
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cn = new SqlCommand();
            cn.CommandText = "insert into T_Base_CarParameter (Level,Engine,Powertype,Gearbox,LWG,Bodystructure,Listingyear,Topspeed,accelerate) output inserted.Id " +
                "values (@Level,@Engine,@Powertype,@Gearbox,@LWG,@Bodystructure,@Listingyear,@Topspeed,@accelerate)";
            cn.Parameters.AddWithValue("@Level", model.CarParameter.Level);
            cn.Parameters.AddWithValue("@Engine", model.CarParameter.Engine);
            cn.Parameters.AddWithValue("@Powertype", model.CarParameter.Powertype);
            cn.Parameters.AddWithValue("@Gearbox", model.CarParameter.Gearbox);
            cn.Parameters.AddWithValue("@LWG", model.CarParameter.LWG);
            cn.Parameters.AddWithValue("@Bodystructure", model.CarParameter.Bodystructure);
            cn.Parameters.AddWithValue("@Listingyear", model.CarParameter.Listingyear);
            cn.Parameters.AddWithValue("@Topspeed", model.CarParameter.Topspeed);
            cn.Parameters.AddWithValue("@accelerate", model.CarParameter.accelerate);
            cn.Connection = co;
            int ParId = Convert.ToInt32(cn.ExecuteScalar());

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "insert into T_Base_Car (Brand,Carmodel,Modeldetail,Price,Stock,Color,Image,BasicparameterId) values (@Brand, @Carmodel, @Modeldetail, @Price, @Stock,@Color,@Image,@BasicparameterId)";
            cm.Parameters.AddWithValue("@Brand", model.Brand);
            cm.Parameters.AddWithValue("@Carmodel", model.Carmodel);
            cm.Parameters.AddWithValue("@Modeldetail", model.Modeldetail);
            cm.Parameters.AddWithValue("@Price", model.Price);
            cm.Parameters.AddWithValue("@Stock", model.Stock);
            cm.Parameters.AddWithValue("@Color", model.Color);
            cm.Parameters.AddWithValue("@Image", model.Image);
            cm.Parameters.AddWithValue("@BasicparameterId", ParId);
            cm.Connection = co;
            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public Model.T_Base_Car GetModel(int id)
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

        public int Update(Model.T_Base_Car model)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();
            SqlCommand cn = new SqlCommand();
            cn.CommandText = "update T_Base_CarParameter set Level=@Level,Engine=@Engine,Powertype=@Powertype,Gearbox=@Gearbox,LWG=@LWG,Bodystructure=@Bodystructure,Listingyear=@Listingyear,"+
                "Topspeed=@Topspeed,accelerate=@accelerate where Id=@Id";
            cn.Parameters.AddWithValue("@Level", model.CarParameter.Level);
            cn.Parameters.AddWithValue("@Engine", model.CarParameter.Engine);
            cn.Parameters.AddWithValue("@Powertype", model.CarParameter.Powertype);
            cn.Parameters.AddWithValue("@Gearbox", model.CarParameter.Gearbox);
            cn.Parameters.AddWithValue("@LWG", model.CarParameter.LWG);
            cn.Parameters.AddWithValue("@Bodystructure", model.CarParameter.Bodystructure);
            cn.Parameters.AddWithValue("@Listingyear", model.CarParameter.Listingyear);
            cn.Parameters.AddWithValue("@Topspeed", model.CarParameter.Topspeed);
            cn.Parameters.AddWithValue("@accelerate", model.CarParameter.accelerate);
            cn.Parameters.AddWithValue("@Id", model.CarParameter.Id);
            cn.Connection = co;

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "update T_Base_Car set Brand=@Brand,Carmodel=@Carmodel,Modeldetail=@Modeldetail,Price=@Price,Stock=@Stock,Color=@Color,Image=@Image,BasicparameterId=@BasicparameterId where Id=@Id";
            cm.Parameters.AddWithValue("@Brand", model.Brand);
            cm.Parameters.AddWithValue("@Carmodel", model.Carmodel);
            cm.Parameters.AddWithValue("@Modeldetail", model.Modeldetail);
            cm.Parameters.AddWithValue("@Price", model.Price);
            cm.Parameters.AddWithValue("@Stock", model.Stock);
            cm.Parameters.AddWithValue("@Color", model.Color);
            cm.Parameters.AddWithValue("@Image", model.Image);
            cm.Parameters.AddWithValue("@BasicparameterId",model.BasicparameterId);
            cm.Parameters.AddWithValue("@Id", model.Id);
            cm.Connection = co;

            int result = cm.ExecuteNonQuery()+cn.ExecuteNonQuery();
            co.Close();
            return result;
        }
    }
}
