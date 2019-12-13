using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4S.WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public JsonResult LoginCheck(string username, string password)
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
                Session["LoginIn"] = username;
                return Json(new { code = 1, message = "登录成功" });
            }
            else
                return Json(new { code = 0, message = "登录失败" });
        }

        public ActionResult main()
        {
            return View();
        }
    }
}