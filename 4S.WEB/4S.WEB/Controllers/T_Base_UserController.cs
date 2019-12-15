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
    }
}