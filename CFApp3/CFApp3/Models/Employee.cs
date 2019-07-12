using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CFApp3.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Guid EmpID { get; set; }

        public string Email { get; set; }

    }
}