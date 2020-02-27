using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Model
{
    public class T_Base_Employee
    {
        public int Id { get; set; }
        public string RealName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Office { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
