using Employee.Common;
using Employee.DAL;
using Employee.Model;
using Employee.MVC.Models;
using Employee.Service;
using Employee.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Employee.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        protected IEmployeeService EmployeeService { get; set; }

        public EmployeeController(IEmployeeService employeeService)
        {
            EmployeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllEmployeeAsync(Paging paging, Filtering filtering, string sorting2)
        {
            try
            {
                Sorting sorting1 = new Sorting();
                sorting1.SortOrder = sorting2;
                List <EmployeeModel> employees = await EmployeeService.GetAllEmployeeAsync(paging, sorting1, filtering);

                List<EmployeeViewModel> employeesViewModel = new List<EmployeeViewModel>();
                
                ViewBag.FirstNameSort = sorting2 == null || String.IsNullOrEmpty(sorting2) ? "FirstName desc" : "";
                ViewBag.LastNameSort = sorting2 == null || String.IsNullOrEmpty(sorting2) ? "LastName desc" : "";
                ViewBag.BirthdaySort = sorting2 == null || String.IsNullOrEmpty(sorting2) ? "Birthday desc" : "";

                if (employees == null)
                {
                    return View("Error");
                }

                foreach (EmployeeModel employee in employees)
                {
                    EmployeeViewModel viewModel = new EmployeeViewModel
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Birthday = employee.Birthday
                    };
                    employeesViewModel.Add(viewModel);
                }
                return View(employeesViewModel);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetEmployeeAsync(Guid id)
        {
            try
            {
                EmployeeModel employee = await EmployeeService.GetEmployeeAsync(id);
                if (employee == null)
                {
                    return View("Error");
                }

                EmployeeViewModel employeeById = new EmployeeViewModel
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Birthday = employee.Birthday
                };
                return View(employeeById);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult PostEmployeeAsync()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> PostEmployeeAsync(EmployeeViewModel employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Error");
                }

                EmployeeModel addEmployee = new EmployeeModel
                {
                    Id = Guid.NewGuid(),
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Birthday = (DateTime)employee.Birthday
                };

                bool success = await EmployeeService.PostEmployeeAsync(addEmployee);
                if (!success)
                {
                    return View("Error");
                }
                return RedirectToAction("GetAllEmployeeAsync");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> PutEmployeeAsync(Guid id)
        {
            try
            {
                EmployeeModel employee = await EmployeeService.GetEmployeeAsync(id);
                if (employee == null)
                {
                    return View("Error");
                }

                EmployeeViewModel employeeById = new EmployeeViewModel
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Birthday = employee.Birthday
                };
                return View(employeeById);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> PutEmployeeAsync(EmployeeViewModel employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Error");
                }

                EmployeeModel editEmployee = new EmployeeModel
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Birthday = employee.Birthday
                };

                bool success = await EmployeeService.PutEmployeeAsync(employee.Id, editEmployee);
                if (!success)
                {
                    return View("Error");
                }
                return RedirectToAction("GetAllEmployeeAsync");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeleteEmployeeAsync(Guid id)
        {
            try
            {
                EmployeeModel employee = await EmployeeService.GetEmployeeAsync(id);
                if (employee == null)
                {
                    return View("Error");
                }

                EmployeeViewModel employeeById = new EmployeeViewModel
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Birthday = employee.Birthday
                };
                return View(employeeById);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost, ActionName("DeleteEmployeeAsync")]
        public async Task<ActionResult> ConfirmationDeleteEmployeeAsync(Guid id)
        {
            bool success = await EmployeeService.DeleteEmployeeAsync(id);
            if (!success)
            {
                return View("Error");
            }

            return RedirectToAction("GetAllEmployeeAsync");
        }
    }
}