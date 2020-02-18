using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Model
{
    public class T_Base_Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Carmodel { get; set; }
        public string Modeldetail { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int Color { get; set; }
        public string Image { get; set; }
        public int BasicparameterId { get; set; }
        public Model.T_Base_CarParameter CarParameter { get; set; }
        
    }
}
