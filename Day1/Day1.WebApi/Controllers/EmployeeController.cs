using Employee.Common;
using Employee.Model;
using Employee.Service;
using Employee.Service.Common;
using Employee.WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Employee.WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        protected IEmployeeService EmployeeService { get; set; }

        public EmployeeController(IEmployeeService employeeService)
        {
            EmployeeService = employeeService;
        }
        
        //IEmployeeService _employeeService = new EmployeeService();

        // GET api/employee
        [Route("api/employee/GetAllEmployeeAsync")]
        public async Task<HttpResponseMessage> GetAllEmployeeAsync([FromUri] Paging paging)
        {
            List<EmployeeModel> employees = await EmployeeService.GetAllEmployeeAsync(paging);

            List<EmployeeRest> employeesRest = new List<EmployeeRest>();

            if (employees == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No employee records found!");
            }

            foreach (EmployeeModel employee in employees)
            {
                EmployeeRest employeeRest = new EmployeeRest();
                employeeRest.FirstName = employee.FirstName;
                employeeRest.LastName = employee.LastName;

                employeesRest.Add(employeeRest);
            }
            return Request.CreateResponse(HttpStatusCode.OK, employees);
            
        }
    


        [HttpGet]
        [Route("api/employee/{id}")]
        public async Task<HttpResponseMessage> GetEmployeeAsync(Guid id)
        {
            EmployeeModel employee = await EmployeeService.GetEmployeeAsync(id);
            if (employee == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No employee records found!");
            }

            EmployeeRest employeeRest = new EmployeeRest();
            employeeRest.FirstName = employee.FirstName;
            employeeRest.LastName = employee.LastName;
            return Request.CreateResponse(HttpStatusCode.OK, employeeRest);
        }

        [HttpPost]
        [Route("api/employee/")]
        public async Task<HttpResponseMessage> PostEmployeeAsync(EmployeeModelPostRest employeeRest)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.FirstName = employeeRest.FirstName;
            employeeModel.LastName = employeeRest.LastName;

            bool success = await EmployeeService.PostEmployeeAsync(employeeModel);
            if (!success)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee addition failed!");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Employee successfully added!");
        }



        [HttpPut]
        [Route("api/employee/{id}")]

        public async Task<HttpResponseMessage> PutEmployeeAsync(Guid id, EmployeeModelPutRest employeeRest)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.FirstName = employeeRest.FirstName;
            employeeModel.LastName = employeeRest.LastName;

            bool success = await EmployeeService.PutEmployeeAsync(id, employeeModel);
            if (!success)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee records update failed!");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Employee successfully updated!");
        }

        [HttpDelete]
        [Route("api/employee/{id}")]
        public async Task<HttpResponseMessage> DeleteEmployeeAsync(Guid id)
        {
            bool success = await EmployeeService.DeleteEmployeeAsync(id);
            if (!success)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee deletion failed!");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Employee successfully deleted!");
        }

        public class EmployeeModelPostRest
        {
            [Required(ErrorMessage = "First name is required!")]
            public string FirstName { get; set; }
            [Required(ErrorMessage ="Last name is required!")]
            public string LastName { get; set; }
        }

        public class EmployeeModelPutRest
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
        
    }
}




