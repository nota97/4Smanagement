using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace _4S.DAL
{
    public class T_Base_CarPart
    {
        public int GetCount()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select count(*) from T_Base_CarPart";
            cm.Connection = co;

            Object count = cm.ExecuteScalar();
            co.Close();
            return (Int32)count;
        }

        public List<Model.T_Base_CarPart> GetlistByPage(int pageSize, int pageNumber, string search, string sortName, string sortOrder, string Brand, string Name)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            int skipcount = (pageNumber - 1) * pageSize;
            string order = " order by " + sortName + " " + sortOrder + " ";

            string where = "  (Brand like '%" + Brand + "%' and  Name like '%" + Name + "%' ) ";
            if (search == "")
            {
                string subtable = "(select top " + skipcount + " Id from T_Base_CarPart where " + where + " " + order + "  )";
                cm.CommandText = "select top " + pageSize + " * from T_Base_CarPart where  " + where + " and id not in " + subtable + " " + order;
            }
            else
            {
                string temp_table = "(select * from T_Base_CarPart where " + "Brand like '%" + search + "%' or  Name like '%" + search + "%') as temp_table";
                string subtable = "(select top " + skipcount + " Id from " + temp_table + " where " + where + " " + order + "  )";
                cm.CommandText = "select top " + pageSize + " * from " + temp_table + "  where  " + where + "and id not in " + subtable + " " + order;
            }

            SqlDataReader dr = cm.ExecuteReader();
            List<Model.T_Base_CarPart> lst = new List<Model.T_Base_CarPart>();
            while (dr.Read())
            {
                Model.T_Base_CarPart user = new Model.T_Base_CarPart();
                user.Id = Convert.ToInt32(dr["Id"]);
                user.Name = Convert.ToString(dr["Name"]);
                user.Brand = Convert.ToString(dr["Brand"]);
                user.Price = Convert.ToDecimal(dr["Price"]);
                user.Applicable = Convert.ToString(dr["Applicable"]);
                user.Note = Convert.ToString(dr["Note"]);
                user.Picture = Convert.ToString(dr["Picture"]);
                user.Stock = Convert.ToInt32(dr["Stock"]);
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
            cm.CommandText = "delete from T_Base_CarPart where id=@id";
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
            cm.CommandText = "delete from T_Base_CarPart where id   in (" + ids + ")";
            cm.Connection = co;

            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public int Add(Model.T_Base_CarPart model)
        {
            //把数据存入数据库
            //ado.net插入数据库
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "insert into T_Base_CarPart (Name,Brand,Price,Stock,Applicable,Note,Picture) values (@Name, @Brand, @Price, @Stock, @Applicable, @Note,@Picture)";
            cm.Parameters.AddWithValue("@Name", model.Name);
            cm.Parameters.AddWithValue("@Brand", model.Brand);
            cm.Parameters.AddWithValue("@Price", model.Price);
            cm.Parameters.AddWithValue("@Applicable", model.Applicable);
            cm.Parameters.AddWithValue("@Note", model.Note);
            cm.Parameters.AddWithValue("@Picture", model.Picture);
            cm.Parameters.AddWithValue("@Stock", model.Stock);
            cm.Connection = co;
            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public Model.T_Base_CarPart GetModel(int id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select * from T_Base_CarPart where id=@id";
            cm.Parameters.AddWithValue("@id", id);

            cm.Connection = co;
            SqlDataReader dr = cm.ExecuteReader();
            Model.T_Base_CarPart model = null;
            while (dr.Read())
            {
                model = new Model.T_Base_CarPart();

                model.Id = Convert.ToInt32(dr["Id"]);
                model.Name = Convert.ToString(dr["Name"]);
                model.Brand = Convert.ToString(dr["Brand"]);
                model.Price = Convert.ToDecimal(dr["Price"]);
                model.Applicable = Convert.ToString(dr["Applicable"]);
                model.Note = Convert.ToString(dr["Note"]);
                model.Picture = Convert.ToString(dr["Picture"]);
                model.Stock = Convert.ToInt32(dr["Stock"]);
            }
            dr.Close();
            co.Close();
            return model;
        }

        public int Update(Model.T_Base_CarPart model)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "update T_Base_CarPart set Name=@Name,Brand=@Brand,Price=@Price,Stock=@Stock,Applicable=@Applicable,Note=@Note,Picture=@Picture where Id=@Id";
            cm.Parameters.AddWithValue("@Name", model.Name);
            cm.Parameters.AddWithValue("@Brand", model.Brand);
            cm.Parameters.AddWithValue("@Price", model.Price);
            cm.Parameters.AddWithValue("@Stock", model.Stock);
            cm.Parameters.AddWithValue("@Applicable", model.Applicable);
            cm.Parameters.AddWithValue("@Note", model.Note);
            cm.Parameters.AddWithValue("@Picture", model.Picture);
            cm.Parameters.AddWithValue("@Id", model.Id);
            cm.Connection = co;

            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }
    }
}
