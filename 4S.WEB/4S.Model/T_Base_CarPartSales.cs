using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Model
{
    public class T_Base_CarPartSales
    {
        public int Id { get; set; }
        public int CarPartId { get; set; }
        public Model.T_Base_CarPart CarPart { get; set; }
        public int Amount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Dtime { get; set; }
        public int UserId { get; set; }
        public Model.T_Base_User User { get; set; }
        public string Seller { get; set; }
    }
}
