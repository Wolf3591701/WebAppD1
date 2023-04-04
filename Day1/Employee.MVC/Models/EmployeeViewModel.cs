using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Employee.MVC.Models
{
    public class EmployeeViewModel
    {
        public Guid Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}