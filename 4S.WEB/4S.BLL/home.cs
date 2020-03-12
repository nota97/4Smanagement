using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.BLL
{
    public class home
    {
        public Model.home GetModel()
        {
            DAL.home dal = new DAL.home();
            return dal.GetModel();
        }

        public List<Model.T_Base_Employee> GetAllList()
        {
            //记录日志
            DAL.home dal = new DAL.home();
            return dal.GetAllList();

        }

        public int Delete(int id)
        {
            DAL.home dal = new DAL.home();
            return dal.Delete(id);
        }

        public int Add(Model.T_Base_Employee model)
        {
            //记录日志
            DAL.home dal = new DAL.home();
            return dal.Add(model);

        }

        public Model.T_Base_Employee GetModel(int id)
        {
            DAL.home dal = new DAL.home();
            return dal.GetModel(id);
        }

        public int Update(Model.T_Base_Employee model)
        {
            DAL.home dal = new DAL.home();
            return dal.Update(model);
        }

        public int SignInCheck(string LoginName, string password)
        {
            DAL.home dal = new DAL.home();
            return dal.SignInCheck(LoginName, password);
        }

        public int LoginCheck(string username, string password)
        {
            DAL.home dal = new DAL.home();
            return dal.LoginCheck(username, password);
        }

        public List<Model.T_Base_Car> GetSomeCars()
        {
            DAL.home dal = new DAL.home();
            return dal.GetSomeCars();
        }
      
        public List<Model.T_Base_Car> GetCars(string carmodel)
        {
            DAL.home dal = new DAL.home();
            return dal.GetCars(carmodel);
        }

        public Model.T_Base_Car GetCarDetail(int id)
        {
            DAL.home dal = new DAL.home();
            return dal.GetCarDetail(id);
        }

        public Model.T_Base_User PersonCenter(int id)
        {
            DAL.home dal = new DAL.home();
            return dal.PersonCenter(id);
        }

        public List<Model.T_Base_Service> GetPersonService(int id)
        {
            DAL.home dal = new DAL.home();
            return dal.GetPersonService(id);
        }

        public List<Model.T_Base_Testdrive> GetPersonReserve(int id)
        {
            DAL.home dal = new DAL.home();
            return dal.GetPersonReserve(id);
        }

        public List<Model.T_Base_CarPart> GetAllCarPart()
        {
            DAL.home dal = new DAL.home();
            return dal.GetAllCarPart();
        }

        public Model.T_Base_CarPart GetCarPartDetail(int id)
        {
            DAL.home dal = new DAL.home();
            return dal.GetCarPartDetail(id);
        }
    }
}
