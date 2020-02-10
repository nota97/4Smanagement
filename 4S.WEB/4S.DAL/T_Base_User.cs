using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace _4S.DAL
{
    public class T_Base_User
    {
        public List<Model.T_Base_User> GetAllList()
        {
            //ado.net
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select * from t_base_user ";
            cm.Connection = co;

            List<Model.T_Base_User> lst = new List<Model.T_Base_User>();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                Model.T_Base_User user = new Model.T_Base_User();
                user.Id = Convert.ToInt32(dr["Id"]);
                user.LoginName = Convert.ToString(dr["LoginName"]);
                user.PWD = Convert.ToString(dr["PWD"]);
                user.Email = Convert.ToString(dr["Email"]);
                user.Type = Convert.ToInt32(dr["Type"]);
                user.Phonenumber = Convert.ToString(dr["Phonenumber"]);
                user.RealName = Convert.ToString(dr["RealName"]);
                lst.Add(user);
            }
            dr.Close();
            co.Close();
            return lst;

        }

        public int GetCount()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select count(*) from T_Base_User";
            cm.Connection = co;

            Object count = cm.ExecuteScalar();
            co.Close();
            return (Int32)count;
        }

        public List<Model.T_Base_User> GetlistByPage(int pageSize, int pageNumber, string search, string sortName, string sortOrder, string Email, string LoginName)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            int skipcount = (pageNumber - 1) * pageSize;
            string order = " order by " + sortName + " " + sortOrder + " ";

            string where = "  (Email like '%" + Email + "%' and  LoginName like '%" + LoginName + "%' ) ";
            if (search == "")
            {
                string subtable = "(select top " + skipcount + " Id from T_Base_User where " + where + " " + order + "  )";
                cm.CommandText = "select top " + pageSize + " * from T_Base_User where  " + where + " and id not in " + subtable + " " + order;
            }
            else
            {
                string temp_table = "(select * from T_Base_User where " + "Email like '%" + search + "%' or  LoginName like '%" + search + "%') as temp_table";
                string subtable = "(select top " + skipcount + " Id from " + temp_table + " where " + where + " " + order + "  )";
                cm.CommandText = "select top " + pageSize + " * from " + temp_table + "  where  " + where + "and id not in " + subtable + " " + order;
            }

            SqlDataReader dr = cm.ExecuteReader();
            List<Model.T_Base_User> lst = new List<Model.T_Base_User>();
            while (dr.Read())
            {
                Model.T_Base_User user = new Model.T_Base_User();
                user.Id = Convert.ToInt32(dr["Id"]);
                user.LoginName = Convert.ToString(dr["LoginName"]);
                user.PWD = Convert.ToString(dr["PWD"]);
                user.Email = Convert.ToString(dr["Email"]);
                user.Type = Convert.ToInt32(dr["Type"]);
                user.Phonenumber = Convert.ToString(dr["Phonenumber"]);
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
            cm.CommandText = "delete from T_Base_User where id=@id";
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
            cm.CommandText = "delete from T_Base_User where id   in (" + ids + ")";
            cm.Connection = co;

            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public int Add(Model.T_Base_User model)
        {
            //把数据存入数据库
            //ado.net插入数据库
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "insert into T_Base_User (LoginName,PWD,RealName,Email,Phonenumber,Type) values (@LoginName, @PWD, @RealName, @Email, @Phonenumber,@Type)";
            cm.Parameters.AddWithValue("@LoginName", model.LoginName);
            cm.Parameters.AddWithValue("@PWD", model.PWD);
            cm.Parameters.AddWithValue("@RealName", model.RealName);
            cm.Parameters.AddWithValue("@Email", model.Email);
            cm.Parameters.AddWithValue("@Phonenumber", model.Phonenumber);
            cm.Parameters.AddWithValue("@Type", model.Type);

            cm.Connection = co;
            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public Model.T_Base_User GetModel(int id)
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
            }
            dr.Close();
            co.Close();
            return model;
        }

        public int Update(Model.T_Base_User model)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "update T_Base_User set LoginName=@LoginName,PWD=@PWD,RealName=@RealName,Email=@Email,Phonenumber=@Phonenumber,Type=@Type where Id=@Id";
            cm.Parameters.AddWithValue("@LoginName", model.LoginName);
            cm.Parameters.AddWithValue("@PWD", model.PWD);
            cm.Parameters.AddWithValue("@RealName", model.RealName);
            cm.Parameters.AddWithValue("@Email", model.Email);
            cm.Parameters.AddWithValue("@Phonenumber", model.Phonenumber);
            cm.Parameters.AddWithValue("@Type", model.Type);
            cm.Parameters.AddWithValue("@Id", model.Id);
            cm.Connection = co;

            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }
    }
}
