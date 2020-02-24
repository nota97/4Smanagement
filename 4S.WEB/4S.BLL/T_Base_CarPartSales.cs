using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.BLL
{
    public class T_Base_CarPartSales
    {
        public int GetCount()
        {
            DAL.T_Base_CarPartSales dal = new DAL.T_Base_CarPartSales();
            return dal.GetCount();
        }

        public List<Model.T_Base_CarPartSales> GetlistByPage(int pageSize, int pageNumber, string search, string sortName, string sortOrder, string RealName, string Name)
        {
            //记录日志
            DAL.T_Base_CarPartSales dal = new DAL.T_Base_CarPartSales();
            return dal.GetlistByPage(pageSize, pageNumber, search, sortName, sortOrder, RealName, Name);

        }

        public int Delete(int id)
        {
            DAL.T_Base_CarPartSales dal = new DAL.T_Base_CarPartSales();
            return dal.Delete(id);
        }

        public int Deletes(string ids)
        {
            //记录日志
            DAL.T_Base_CarPartSales dal = new DAL.T_Base_CarPartSales();
            return dal.Deletes(ids);
        }

        public int Add(Model.T_Base_CarPartSales model)
        {
            //记录日志
            DAL.T_Base_CarPartSales dal = new DAL.T_Base_CarPartSales();
            return dal.Add(model);
        }

        public Model.T_Base_CarPartSales GetModel(int id)
        {
            DAL.T_Base_CarPartSales dal = new DAL.T_Base_CarPartSales();
            return dal.GetModel(id);
        }

        public int Update(Model.T_Base_CarPartSales model)
        {
            DAL.T_Base_CarPartSales dal = new DAL.T_Base_CarPartSales();
            return dal.Update(model);
        }
    }
}
