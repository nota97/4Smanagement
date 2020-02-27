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
            BLL.home bll = new BLL.home();
            List<Model.T_Base_Car> lst = bll.GetSomeCars();
            ViewBag.lst = lst;
            ViewBag.data = Session["SignIn"];
            ViewBag.Id = Session["Id"];
            return View("Index", "~/Views/Shared/_Layout.cshtml");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.data = Session["SignIn"];
            return View("About", "~/Views/Shared/_Layout.cshtml");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View("Contact", "~/Views/Shared/_Layout.cshtml");
        }

        public ActionResult SignIn()
        {
            //Session["SignIn"] = null;
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }


        public ActionResult Login()
        {
            Session["LoginIn"] = null;
            return View();
        }

        public JsonResult SignInCheck(string LoginName, string password)
        {
            BLL.home model = new BLL.home();
            int result=model.SignInCheck(LoginName, password);
            if (result > 0)
            {
                Session["SignIn"] = LoginName;
                Session["Id"] = result;
                return Json(new { code = 1, message = "登录成功" });
            }
            else
            {
                return Json(new { code = 0, message = "登录失败" });
            }
        }

        public JsonResult LoginCheck(string username, string password)
        {
            BLL.home model = new BLL.home();
            int result = model.LoginCheck(username, password);
            if (result > 0)
            {
                Session["LoginIn"] = username;
                return Json(new { code = 1, message = "登录成功" });
            }
            else
            {
                return Json(new { code = 0, message = "登录失败" });
            } 
        }

        public ActionResult main()
        {
            if (Session["LoginIn"] == null)
            {
                return Redirect("/home/login");
            }
            ViewBag.data = Session["LoginIn"];
            Model.home model = new Model.home();
            BLL.home bll = new BLL.home();
            model = bll.GetModel();
            ViewBag.model = model;

            List<Model.T_Base_Employee> lst = bll.GetAllList();
            ViewBag.lst = lst;
            return View();
        }

        public JsonResult Delete(int id)
        {
            BLL.home bll = new BLL.home();
            int result = bll.Delete(id);
            if (result > 0)
            {
                return Json(new { code = 1, message = "删除成功" });
            }
            else
                return Json(new { code = 0, message = "删除失败" });
        }

        public ActionResult Add()
        {
            if (Session["LoginIn"] == null)
            {
                return Redirect("/home/login");
            }
            ViewBag.data = Session["LoginIn"];
            return View("add");
        }

        public JsonResult AddSave(Model.T_Base_Employee model)
        {
            //处理
            BLL.home bll = new BLL.home();
            int result = bll.Add(model);
            if (result >= 1)
            {
                return Json(new { code = 1, message = "插入成功" });
            }
            else
            {
                return Json(new { code = 0, message = "插入失败" });
            }
        }

        public ActionResult Edit(int id)
        {
            if (Session["LoginIn"] == null)
            {
                return Redirect("/home/login");
            }
            ViewBag.data = Session["LoginIn"];
            Model.T_Base_Employee model = new Model.T_Base_Employee();
            BLL.home bll = new BLL.home();
            model = bll.GetModel(id);
            ViewBag.model = model;
            return View();
        }

        public JsonResult EditSave(Model.T_Base_Employee model)
        {
            BLL.home bll = new BLL.home();
            int result = bll.Update(model);
            if (result > 0)
            {
                return Json(new { code = 1, message = " 修改成功" });
            }
            else
                return Json(new { code = 0, message = "修改失败" });
        }
    }
}