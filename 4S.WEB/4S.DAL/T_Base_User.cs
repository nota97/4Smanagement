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
            cm.CommandText = "select * from t_base_user";
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
    }
}
