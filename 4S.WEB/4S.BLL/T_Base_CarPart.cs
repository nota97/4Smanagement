using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.BLL
{
    public class T_Base_CarPart
    {
        public int GetCount()
        {
            DAL.T_Base_CarPart dal = new DAL.T_Base_CarPart();
            return dal.GetCount();
        }

        public List<Model.T_Base_CarPart> GetlistByPage(int pageSize, int pageNumber, string search, string sortName, string sortOrder, string Email, string LoginName)
        {
            //记录日志
            DAL.T_Base_CarPart dal = new DAL.T_Base_CarPart();
            return dal.GetlistByPage(pageSize, pageNumber, search, sortName, sortOrder, Email, LoginName);

        }

        public int Delete(int id)
        {
            DAL.T_Base_CarPart dal = new DAL.T_Base_CarPart();
            return dal.Delete(id);
        }

        public int Deletes(string ids)
        {
            //记录日志
            DAL.T_Base_CarPart dal = new DAL.T_Base_CarPart();
            return dal.Deletes(ids);
        }

        public int Add(Model.T_Base_CarPart model)
        {
            //记录日志
            DAL.T_Base_CarPart dal = new DAL.T_Base_CarPart();
            return dal.Add(model);

        }

        public Model.T_Base_CarPart GetModel(int id)
        {
            DAL.T_Base_CarPart dal = new DAL.T_Base_CarPart();
            return dal.GetModel(id);
        }

        public int Update(Model.T_Base_CarPart model)
        {
            DAL.T_Base_CarPart dal = new DAL.T_Base_CarPart();
            return dal.Update(model);
        }
    }
}
