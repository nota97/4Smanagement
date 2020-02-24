using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.BLL
{
    public class T_Base_Testdrive
    {
        public int GetCount()
        {
            DAL.T_Base_Testdrive dal = new DAL.T_Base_Testdrive();
            return dal.GetCount();
        }

        public List<Model.T_Base_Testdrive> GetlistByPage(int pageSize, int pageNumber, string search, string sortName, string sortOrder, string RealName, string Carmodel)
        {
            //记录日志
            DAL.T_Base_Testdrive dal = new DAL.T_Base_Testdrive();
            return dal.GetlistByPage(pageSize, pageNumber, search, sortName, sortOrder, RealName, Carmodel);

        }

        public int Delete(int id)
        {
            DAL.T_Base_Testdrive dal = new DAL.T_Base_Testdrive();
            return dal.Delete(id);
        }

        public int Deletes(string ids)
        {
            //记录日志
            DAL.T_Base_Testdrive dal = new DAL.T_Base_Testdrive();
            return dal.Deletes(ids);
        }

        public int Add(Model.T_Base_Testdrive model)
        {
            //记录日志
            DAL.T_Base_Testdrive dal = new DAL.T_Base_Testdrive();
            return dal.Add(model);
        }

        public Model.T_Base_Testdrive GetModel(int id)
        {
            DAL.T_Base_Testdrive dal = new DAL.T_Base_Testdrive();
            return dal.GetModel(id);
        }

        public int Update(Model.T_Base_Testdrive model)
        {
            DAL.T_Base_Testdrive dal = new DAL.T_Base_Testdrive();
            return dal.Update(model);
        }
    }
}
