using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Model
{
    public class T_Base_Service
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string CarModel { get; set; }
        public string Modeldetail { get; set; }
        public int UserId { get; set; }
        public Model.T_Base_User user { get; set; }
        public string FaultType { get; set; }
        public DateTime Appointment { get; set; }
        public int DealSituation { get; set; }
        public string Note { get; set; }
        public decimal Price { get; set; }
    }
}
