using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.BLL
{
    public class T_Base_Service
    {
        public int GetCount()
        {
            DAL.T_Base_Service dal = new DAL.T_Base_Service();
            return dal.GetCount();
        }

        public List<Model.T_Base_Service> GetlistByPage(int pageSize, int pageNumber, string search, string sortName, string sortOrder, string RealName, string Carmodel)
        {
            //记录日志
            DAL.T_Base_Service dal = new DAL.T_Base_Service();
            return dal.GetlistByPage(pageSize, pageNumber, search, sortName, sortOrder, RealName, Carmodel);

        }

        public int Delete(int id)
        {
            DAL.T_Base_Service dal = new DAL.T_Base_Service();
            return dal.Delete(id);
        }

        public int Deletes(string ids)
        {
            //记录日志
            DAL.T_Base_Service dal = new DAL.T_Base_Service();
            return dal.Deletes(ids);
        }

        public int Add(Model.T_Base_Service model)
        {
            //记录日志
            DAL.T_Base_Service dal = new DAL.T_Base_Service();
            return dal.Add(model);
        }

        public Model.T_Base_Service GetModel(int id)
        {
            DAL.T_Base_Service dal = new DAL.T_Base_Service();
            return dal.GetModel(id);
        }

        public int Update(Model.T_Base_Service model)
        {
            DAL.T_Base_Service dal = new DAL.T_Base_Service();
            return dal.Update(model);
        }
    }
}
