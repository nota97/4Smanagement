using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4S.WEB.Controllers
{
    public class T_Base_SalesOrderController : Controller
    {
        // GET: T_Base_SalesOrder
        public ActionResult List()
        {
            if (Session["LoginIn"] == null)
            {
                return Redirect("/home/login");
            }
            ViewBag.data = Session["LoginIn"];
            return View();
        }

        public JsonResult getlistByPage(int pageSize, int pageNumber, string sortName, string sortOrder, string search = "", string RealName = "", string Carmodel = "")
        {
            BLL.T_Base_SalesOrder bll = new BLL.T_Base_SalesOrder();
            List<Model.T_Base_SalesOrder> lst = bll.GetlistByPage(pageSize, pageNumber, search, sortName, sortOrder, RealName, Carmodel);
            int totalcount = bll.GetCount();
            var result = new { rows = lst, total = totalcount };
            return Json(result);
        }

        public JsonResult Delete(int id)
        {
            BLL.T_Base_SalesOrder bll = new BLL.T_Base_SalesOrder();
            int result = bll.Delete(id);
            if (result > 1)
            {
                return Json(new { code = 1, message = "删除成功" });
            }
            else
                return Json(new { code = 0, message = "删除失败" });

        }

        public JsonResult Deletes(string ids)
        {
            BLL.T_Base_SalesOrder bll = new BLL.T_Base_SalesOrder();
            int result = bll.Deletes(ids);
            if (result >= 2)
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

        public JsonResult AddSave(Model.T_Base_SalesOrder model)
        {
            //处理
            BLL.T_Base_SalesOrder bll = new BLL.T_Base_SalesOrder();
            int result = bll.Add(model);
            if (result >= 1)
            {
                return Json(new { code = 1, message = "插入成功" });
            }
            else if (result == -1)
            {
                return Json(new { code = 0, message = "库存不足" });
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
            Model.T_Base_SalesOrder model = new Model.T_Base_SalesOrder();
            BLL.T_Base_SalesOrder bll = new BLL.T_Base_SalesOrder();
            model = bll.GetModel(id);
            ViewBag.model = model;
            return View();
        }

        public JsonResult EditSave(Model.T_Base_SalesOrder model)
        {
            BLL.T_Base_SalesOrder bll = new BLL.T_Base_SalesOrder();
            int result = bll.Update(model);
            if (result > 0)
            {
                return Json(new { code = 1, message = " 修改成功" });
            }
            else if (result == -1)
            {
                return Json(new { code = 0, message = "修改失败,库存不足" });
            }
            else
                return Json(new { code = 0, message = "修改失败" });
        }
    }
}