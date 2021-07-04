using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tabels;

namespace webAPI.Models
{
    public class EmployeeModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public System.DateTime date_of_hiring { get; set; }
        public string sex { get; set; }

        public int department { get; set; }
        public string departmentName { get; set; }

    }
}