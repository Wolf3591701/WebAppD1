using Employee.Common;
using Employee.DAL;
using Employee.Model;
using Employee.MVC.Models;
using Employee.Service;
using Employee.Service.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Employee.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        protected IEmployeeService EmployeeService {  get; set; }

        public EmployeeController(IEmployeeService employeeService)
        {
            EmployeeService = employeeService;
        }

        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetAllEmployeeAsync(Paging paging, Sorting sorting, Filtering filtering)
        {
            List<EmployeeModel> employees = await EmployeeService.GetAllEmployeeAsync(paging, sorting, filtering);

            List<EmployeeViewModel> employeesViewModel = new List<EmployeeViewModel>();

            foreach (EmployeeModel employee in employees)
            {
                EmployeeViewModel viewModel = new EmployeeViewModel
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName
                };
                employeesViewModel.Add(viewModel);
            }
            return View(employeesViewModel);
        }
    }
}