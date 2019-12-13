using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Model
{
    public class T_Base_User
    {
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string PWD { get; set; }
        public int Type { get; set; }
    }
}
