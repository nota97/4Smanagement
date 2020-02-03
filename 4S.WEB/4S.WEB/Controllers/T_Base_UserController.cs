using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4S.WEB.Controllers
{
    public class T_Base_UserController : Controller
    {
        // GET: T_Base_User
        public ActionResult List()
        {
            BLL.T_Base_User bllUser = new BLL.T_Base_User();
            List<Model.T_Base_User> lst = bllUser.GetAllList();

            ViewBag.lst = lst;
            return View();
        }

        public JsonResult GetSearch(string search)
        {
            BLL.T_Base_User bll = new BLL.T_Base_User();
            List<Model.T_Base_User> lst = bll.GetList(search);
            var result = new { rows = lst };
            ViewBag.lst = lst;
            return Json(result);
        }

        public JsonResult getlistByPage(int pageSize, int pageNumber, string sortName, string sortOrder, string search = "", string Email = "", string LoginName = "")
        {
            BLL.T_Base_User bll = new BLL.T_Base_User();
            List<Model.T_Base_User> lst = bll.GetlistByPage(pageSize, pageNumber, search, sortName, sortOrder, Email, LoginName);
            int totalcount = bll.GetCount();
            var result = new { rows = lst, total = totalcount };
            return Json(result);
        }
    }
}