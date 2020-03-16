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
            if (Session["Id"] == null)
            {
                ViewBag.Id = 0;
            }
            else
            {
                ViewBag.Id = Session["Id"];
            }
            return View("Index", "~/Views/Shared/_Layout.cshtml");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.data = Session["SignIn"];
            if (Session["Id"] == null)
            {
                ViewBag.Id = 0;
            }
            else
            {
                ViewBag.Id = Session["Id"];
            }
            return View("About", "~/Views/Shared/_Layout.cshtml");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.data = Session["SignIn"];
            if (Session["Id"] == null)
            {
                ViewBag.Id = 0;
            }
            else
            {
                ViewBag.Id = Session["Id"];
            }
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

        public JsonResult Layout()
        {
            Session["SignIn"] = null;
            Session["Id"] = null;
            return Json(new { code = 1, message = "登出成功" });
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

        public ActionResult list()
        {
            BLL.home bll = new BLL.home();
            List<Model.T_Base_Car> Car718 = bll.GetCars("718");
            List<Model.T_Base_Car> Car911 = bll.GetCars("911");
            List<Model.T_Base_Car> CarPanamera = bll.GetCars("Panamera");
            List<Model.T_Base_Car> CarMacan = bll.GetCars("Macan");
            List<Model.T_Base_Car> CarCayenne = bll.GetCars("Cayenne");
            ViewBag.Car718 = Car718;
            ViewBag.Car911 = Car911;
            ViewBag.CarPanamera = CarPanamera;
            ViewBag.CarMacan = CarMacan;
            ViewBag.CarCayenne = CarCayenne;
            ViewBag.data = Session["SignIn"];
            if (Session["Id"] == null)
            {
                ViewBag.Id = 0;
            }
            else
            {
                ViewBag.Id = Session["Id"];
            }
            return View("List", "~/Views/Shared/_Layout.cshtml");
        }

        public ActionResult detail(int id)
        {
            BLL.home bll = new BLL.home();
            Model.T_Base_Car CarDetail = bll.GetCarDetail(id);
            ViewBag.CarDetail = CarDetail;
            ViewBag.data = Session["SignIn"];
            if (Session["Id"] == null)
            {
                ViewBag.Id = 0;
            }
            else
            {
                ViewBag.Id = Session["Id"];
            }          
            return View("detail", "~/Views/Shared/_Layout.cshtml");
        }

        public ActionResult Service()
        {
            ViewBag.data = Session["SignIn"];
            if (Session["Id"] == null)
            {
                ViewBag.Id = 0;
            }
            else
            {
                ViewBag.Id = Session["Id"];
            }
            return View("Service", "~/Views/Shared/_Layout.cshtml");
        }

        public ActionResult PersonCenter(int id)
        {
            if (Session["SignIn"] == null)
            {
                return Redirect("/home/signin");
            }
            BLL.home bll = new BLL.home();
            Model.T_Base_User user = bll.PersonCenter(id);
            List<Model.T_Base_Service> service = bll.GetPersonService(id);
            List<Model.T_Base_Testdrive> reserve = bll.GetPersonReserve(id);
            ViewBag.reserve = reserve;
            ViewBag.service = service;
            ViewBag.user = user;
            ViewBag.data = Session["SignIn"];
            if (Session["Id"] == null)
            {
                ViewBag.Id = 0;
            }
            else
            {
                ViewBag.Id = Session["Id"];
            }
            return View("PersonCenter", "~/Views/Shared/_Layout.cshtml");
        }

        public ActionResult PersonCenterEdit(int id)
        {
            if (Session["SignIn"] == null)
            {
                return Redirect("/home/signin");
            }
            BLL.home bll = new BLL.home();
            Model.T_Base_User user = bll.PersonCenter(id);           
            ViewBag.user = user;
            ViewBag.data = Session["SignIn"];
            if (Session["Id"] == null)
            {
                ViewBag.Id = 0;
            }
            else
            {
                ViewBag.Id = Session["Id"];
            }
            return View("PersonCenterEdit", "~/Views/Shared/_Layout.cshtml");
        }

        public ActionResult partlist()
        {
            BLL.home bll = new BLL.home();
            List<Model.T_Base_CarPart> CarPart = bll.GetAllCarPart();
            ViewBag.CarPart = CarPart;
            ViewBag.data = Session["SignIn"];
            if (Session["Id"] == null)
            {
                ViewBag.Id = 0;
            }
            else
            {
                ViewBag.Id = Session["Id"];
            }
            return View("partlist", "~/Views/Shared/_Layout.cshtml");
        }

        public ActionResult Partdetail(int id)
        {
            BLL.home bll = new BLL.home();
            Model.T_Base_CarPart CarPartDetail = bll.GetCarPartDetail(id);
            ViewBag.CarPartDetail = CarPartDetail;
            ViewBag.data = Session["SignIn"];
            if (Session["Id"] == null)
            {
                ViewBag.Id = 0;
            }
            else
            {
                ViewBag.Id = Session["Id"];
            }
            return View("Partdetail", "~/Views/Shared/_Layout.cshtml");
        }

        public ActionResult Search(string search)
        {
            BLL.T_Base_Car bll = new BLL.T_Base_Car();
            List<Model.T_Base_Car> lst = bll.GetPartList(search);
            ViewBag.lst = lst;
            ViewBag.data = Session["SignIn"];
            if (Session["Id"] == null)
            {
                ViewBag.Id = 0;
            }
            else
            {
                ViewBag.Id = Session["Id"];
            }
            return View("Search", "~/Views/Shared/_Layout.cshtml");
        }
    }
}