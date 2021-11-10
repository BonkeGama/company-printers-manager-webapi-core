using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyPrinters.Models;

namespace CompanyPrinters.Models
{
    public class Login
    {
        public string UserName
        {
            set;
            get;
        }
        public string Password
        {
            set;
            get;
        }
    }
    public class Registration : Employeemaster { }
}
