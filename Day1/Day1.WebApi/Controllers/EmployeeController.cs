using Employee.Model;
using Employee.Service;
using Employee.Service.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Employee.WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        public static string connectionString = "Data Source=DESKTOP-DG2UJNT;Initial Catalog=RentCar;Integrated Security=True";
        IEmployeeService _employeeService = new EmployeeService();

        // GET api/employee
        [Route("api/employee/GetAllEmployeeAsync")]
        public async Task<HttpResponseMessage> GetAllEmployeeAsync()
        {
            List<EmployeeModel> employees = await _employeeService.GetAllEmployeeAsync();

            if (employees == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No employee records found!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, employees);
            }
        }
    


        [HttpGet]
        [Route("api/employee/{id}")]
        public async Task<HttpResponseMessage> GetEmployeeAsync(Guid id)
        {
            EmployeeModel employee = await _employeeService.GetEmployeeAsync(id);
            if (employee == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No employee records found!");
            }

            return Request.CreateResponse(HttpStatusCode.OK, employee);
           
        }

        [HttpPost]
        [Route("api/employee/")]
        public async Task<HttpResponseMessage> PostEmployeeAsync(EmployeeModel employee)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            bool success = await _employeeService.PostEmployeeAsync(employee);
            if (!success)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee addition failed!");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Employee successfully added!");
        }



        [HttpPut]
        [Route("api/employee/{id}")]

        public async Task<HttpResponseMessage> PutEmployeeAsync(Guid id, EmployeeModel employee)
        {
            bool success = await _employeeService.PutEmployeeAsync(id, employee);
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
            bool success = await _employeeService.DeleteEmployeeAsync(id);
            if (!success)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee deletion failed!");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Employee successfully deleted!");
        }
        
    }
}




