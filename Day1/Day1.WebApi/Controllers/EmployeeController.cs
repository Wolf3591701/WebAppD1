using Employee.Model;
using Employee.Service;
using Employee.Service.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Employee.WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        public static string connectionString = "Data Source=DESKTOP-DG2UJNT;Initial Catalog=RentCar;Integrated Security=True";
        IEmployeeService _employeeService = new EmployeeService();

        // GET api/employee
        [Route("api/employee/GetAllEmployee")]
        public HttpResponseMessage GetAllEmployee()
        {
            List<EmployeeModel> employees = _employeeService.GetAllEmployee();
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
        public HttpResponseMessage GetEmployee(Guid id)
        {
            EmployeeModel employee = _employeeService.GetEmployee(id);
            if (employee == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No employee records found!");
            }

            return Request.CreateResponse(HttpStatusCode.OK, employee);
           
        }

        [HttpPost]
        [Route("api/employee/")]
        public HttpResponseMessage PostEmployee(EmployeeModel employee)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            bool success = _employeeService.PostEmployee(employee);
            if (!success)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee addition failed!");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Employee successfully added!");
        }



        [HttpPut]
        [Route("api/employee")]

        public HttpResponseMessage PutEmployee(Guid id, EmployeeModel employee)
        {
            bool success = _employeeService.PutEmployee(id, employee);
            if (!success)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee records update failed!");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Employee successfully updated!");
        }

        [HttpDelete]
        [Route("api/employee/{id}")]
        public HttpResponseMessage DeleteEmployee(Guid id)
        {
            bool success = _employeeService.DeleteEmployee(id);
            if (!success)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee deletion failed!");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Employee successfully deleted!");
        }
        
    }
}




