using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.BLL
{
    public class T_Base_Car
    {
        public int GetCount()
        {
            DAL.T_Base_Car dal = new DAL.T_Base_Car();
            return dal.GetCount();
        }

        public List<Model.T_Base_Car> GetlistByPage(int pageSize, int pageNumber, string search, string sortName, string sortOrder, string Brand, string Carmodel)
        {
            //记录日志
            DAL.T_Base_Car dal = new DAL.T_Base_Car();
            return dal.GetlistByPage(pageSize, pageNumber, search, sortName, sortOrder, Brand, Carmodel);

        }

        public int Delete(int id,int BasicparameterId)
        {
            DAL.T_Base_Car dal = new DAL.T_Base_Car();
            return dal.Delete(id, BasicparameterId);
        }

        public int Deletes(string ids,string BasicparameterIds)
        {
            //记录日志
            DAL.T_Base_Car dal = new DAL.T_Base_Car();
            return dal.Deletes(ids, BasicparameterIds);
        }

        public int Add(Model.T_Base_Car model)
        {
            //记录日志
            DAL.T_Base_Car dal = new DAL.T_Base_Car();
            return dal.Add(model);
        }

        public Model.T_Base_Car GetModel(int id)
        {
            DAL.T_Base_Car dal = new DAL.T_Base_Car();
            return dal.GetModel(id);
        }

        public int Update(Model.T_Base_Car model)
        {
            DAL.T_Base_Car dal = new DAL.T_Base_Car();
            return dal.Update(model);
        }

        public List<Model.T_Base_Car> GetPartList(string search)
        {
            DAL.T_Base_Car dal = new DAL.T_Base_Car();
            return dal.GetPartList(search);
        }
    }
}
