using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.BLL
{
    public class T_Base_User
    {
        public List<Model.T_Base_User> GetAllList()
        {
            //记录日志
            DAL.T_Base_User userDal = new DAL.T_Base_User();
            return userDal.GetAllList();

        }

        public List<Model.T_Base_User> GetList(string search)
        {
            //记录日志
            DAL.T_Base_User userDal = new DAL.T_Base_User();
            return userDal.GetList(search);

        }

        public int GetCount()
        {
            DAL.T_Base_User dal = new DAL.T_Base_User();
            return dal.GetCount();
        }
        public List<Model.T_Base_User> GetlistByPage(int pageSize, int pageNumber, string search, string sortName, string sortOrder, string Email, string LoginName)
        {
            //记录日志
            DAL.T_Base_User dal = new DAL.T_Base_User();
            return dal.GetlistByPage(pageSize, pageNumber, search, sortName, sortOrder, Email, LoginName);

        }
    }
}
