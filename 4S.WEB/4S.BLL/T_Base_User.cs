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

        public int Delete(int id)
        {
            DAL.T_Base_User dal = new DAL.T_Base_User();
            return dal.Delete(id);
        }

        public int Deletes(string ids)
        {
            //记录日志
            DAL.T_Base_User dal = new DAL.T_Base_User();
            return dal.Deletes(ids);
        }

        public int Add(Model.T_Base_User model)
        {
            //记录日志
            DAL.T_Base_User dal = new DAL.T_Base_User();
            return dal.Add(model);

        }

        public Model.T_Base_User GetModel(int id)
        {
            DAL.T_Base_User dal = new DAL.T_Base_User();
            return dal.GetModel(id);
        }

        public int Update(Model.T_Base_User model)
        {
            DAL.T_Base_User dal = new DAL.T_Base_User();
            return dal.Update(model);
        }
    }
}
