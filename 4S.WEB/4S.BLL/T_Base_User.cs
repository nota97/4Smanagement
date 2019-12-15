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
    }
}
