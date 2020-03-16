using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4S.WEB.Controllers
{
    public class T_Base_CarController : Controller
    {
        // GET: T_Base_Car
        public ActionResult List()
        {
            if (Session["LoginIn"] == null)
            {
                return Redirect("/home/login");
            }
            ViewBag.data = Session["LoginIn"];
            return View();
        }

        public JsonResult getlistByPage(int pageSize, int pageNumber, string sortName, string sortOrder, string search = "", string Brand = "", string Carmodel = "")
        {
            BLL.T_Base_Car bll = new BLL.T_Base_Car();
            List<Model.T_Base_Car> lst = bll.GetlistByPage(pageSize, pageNumber, search, sortName, sortOrder, Brand, Carmodel);
            int totalcount = bll.GetCount();
            var result = new { rows = lst, total = totalcount };
            return Json(result);
        }

        public JsonResult Delete(int id,int BasicparameterId)
        {
            BLL.T_Base_Car bll = new BLL.T_Base_Car();
            int result = bll.Delete(id, BasicparameterId);
            if (result > 0)
            {
                return Json(new { code = 1, message = "删除成功" });
            }
            else
                return Json(new { code = 0, message = "删除失败" });

        }

        public JsonResult Deletes(string ids,string BasicparameterIds)
        {
            BLL.T_Base_Car bll = new BLL.T_Base_Car();
            int result = bll.Deletes(ids, BasicparameterIds);
            if (result > 1)
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

        public JsonResult AddSave(Model.T_Base_Car model)
        {
            //处理
            BLL.T_Base_Car bll = new BLL.T_Base_Car();
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
            Model.T_Base_Car model = new Model.T_Base_Car();
            BLL.T_Base_Car bll = new BLL.T_Base_Car();
            model = bll.GetModel(id);
            ViewBag.model = model;
            return View();
        }

        public JsonResult EditSave(Model.T_Base_Car model)
        {
            BLL.T_Base_Car bll = new BLL.T_Base_Car();
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