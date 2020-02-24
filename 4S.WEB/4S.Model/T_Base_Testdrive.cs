using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Model
{
    public class T_Base_Testdrive
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public Model.T_Base_Car Car { get; set; }
        public int UserId { get; set; }
        public Model.T_Base_User User { get; set; }
        public DateTime Dtime { get; set; }
        public int DealSituation { get; set; }
        public string DealPerson { get; set; }
    }
}
