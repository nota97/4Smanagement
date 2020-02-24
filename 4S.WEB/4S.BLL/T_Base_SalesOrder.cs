using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.BLL
{
    public class T_Base_SalesOrder
    {
        public int GetCount()
        {
            DAL.T_Base_SalesOrder dal = new DAL.T_Base_SalesOrder();
            return dal.GetCount();
        }

        public List<Model.T_Base_SalesOrder> GetlistByPage(int pageSize, int pageNumber, string search, string sortName, string sortOrder, string RealName, string Carmodel)
        {
            //记录日志
            DAL.T_Base_SalesOrder dal = new DAL.T_Base_SalesOrder();
            return dal.GetlistByPage(pageSize, pageNumber, search, sortName, sortOrder, RealName, Carmodel);

        }

        public int Delete(int id)
        {
            DAL.T_Base_SalesOrder dal = new DAL.T_Base_SalesOrder();
            return dal.Delete(id);
        }

        public int Deletes(string ids)
        {
            //记录日志
            DAL.T_Base_SalesOrder dal = new DAL.T_Base_SalesOrder();
            return dal.Deletes(ids);
        }

        public int Add(Model.T_Base_SalesOrder model)
        {
            //记录日志
            DAL.T_Base_SalesOrder dal = new DAL.T_Base_SalesOrder();
            return dal.Add(model);
        }

        public Model.T_Base_SalesOrder GetModel(int id)
        {
            DAL.T_Base_SalesOrder dal = new DAL.T_Base_SalesOrder();
            return dal.GetModel(id);
        }

        public int Update(Model.T_Base_SalesOrder model)
        {
            DAL.T_Base_SalesOrder dal = new DAL.T_Base_SalesOrder();
            return dal.Update(model);
        }
    }
}
