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
                lst.Add(user);
            }
            dr.Close();
            co.Close();
            return lst;

        }

        public List<Model.T_Base_User> GetList(string seacrh)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select * from t_base_user where LoginName like '%" + seacrh + "%'";
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
                lst.Add(user);           
            }
            dr.Close();
            co.Close();
            return lst;
        }
    }
}
