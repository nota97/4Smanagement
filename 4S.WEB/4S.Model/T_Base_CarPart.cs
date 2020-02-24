using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Model
{
    public class T_Base_CarPart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public string Applicable { get; set; }
        public string Note { get; set; }
        public string Picture { get; set; }
        public int Stock { get; set; }
    }
}
