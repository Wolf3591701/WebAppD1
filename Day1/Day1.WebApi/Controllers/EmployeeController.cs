using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;

namespace Day1.WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        static string connectionString = "Data Source=DESKTOP-DG2UJNT;Initial Catalog=RentCar;Integrated Security=True";


        // GET api/values
        [Route("api/values/GetAllEmployee")]
        public HttpResponseMessage GetAllEmployee()
        {
            SqlConnection connection = new SqlConnection(connectionString);


            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT * FROM EMPLOYEE;", connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Employee> list = new List<Employee>();

                while (reader.Read())
                {
                    Employee emp = new Employee();

                    emp.Id = reader.GetGuid(0);
                    emp.FirstName = reader.GetString(1);
                    emp.LastName = reader.GetString(2);
                    emp.Birthday = reader.GetDateTime(3);

                    list.Add(emp);

                }

                if (reader.HasRows)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, list);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No employee records found!");
                }

            }
        }

        // GET api/values
        [Route("api/values/GetEmployeeId")]
        public HttpResponseMessage GetEmployeeId()
        {
            SqlConnection connection = new SqlConnection(connectionString);


            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT * FROM EMPLOYEE WHERE Id=@Id;", connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Employee> list = new List<Employee>();

                while (reader.Read())
                {
                    Employee emp = new Employee();

                    emp.Id = reader.GetGuid(0);
                    emp.FirstName = reader.GetString(1);
                    emp.LastName = reader.GetString(2);
                    emp.Birthday = reader.GetDateTime(3);

                    list.Add(emp);

                }

                if (reader.HasRows)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, list);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No employee records found!");
                }

            }
        }
        // POST api/values
        [Route("api/values/PostEmployee")]
        public HttpResponseMessage PostEmployee(Employee employee)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand("INSERT INTO EMPLOYEE VALUES (@Id,@FirstName,@LastName,@Birthday);", connection);

                connection.Open();

                employee.Id = Guid.NewGuid();
                command.Parameters.AddWithValue("@Id", employee.Id);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@Lastname", employee.LastName);
                command.Parameters.AddWithValue("@Birthday", employee.Birthday);

                int numberOfRowsAffected = command.ExecuteNonQuery();

                if (numberOfRowsAffected > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Employee successfully added!");   
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Employee entry failed!"); 
                }

                
            }
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
