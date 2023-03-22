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

                    connection.Close();
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
        [Route("api/values/GetEmployeeId/{id}")]
        public HttpResponseMessage GetEmployeeId(Guid id)
        {

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);

                using (connection)
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM EMPLOYEE WHERE Id=@Id;", connection);

                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    Employee emp = new Employee();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        emp.Id = reader.GetGuid(0);
                        emp.FirstName = reader.GetString(1);
                        emp.LastName = reader.GetString(2);
                        emp.Birthday = reader.GetDateTime(3);

                        connection.Close();
                        return Request.CreateResponse(HttpStatusCode.OK, emp);
                    }

                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No employee with ID: {id}");
                    }

                }
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong, contact system admin!");
            }
        }

        // POST api/values
        [Route("api/values/PostEmployee")]
        public HttpResponseMessage PostEmployee(Employee employee)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);

                using (connection)
                {
                    SqlCommand command = new SqlCommand("INSERT INTO EMPLOYEE VALUES (@Id,@FirstName,@LastName,@Birthday);", connection);

                    employee.Id = Guid.NewGuid();
                    command.Parameters.AddWithValue("@Id", employee.Id);
                    command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    command.Parameters.AddWithValue("@Lastname", employee.LastName);
                    command.Parameters.AddWithValue("@Birthday", employee.Birthday);

                    connection.Open();

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
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong, contact system admin!");
            }
        }

        // PUT api/values/5
        [Route("api/values/PutEmployee")]
        public HttpResponseMessage Put(Guid id, Employee employee)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);

                using (connection)
                {
                    SqlCommand commandSelect = new SqlCommand("SELECT * FROM EMPLOYEE WHERE Id=@Id;", connection);

                    commandSelect.Parameters.AddWithValue("@id", id);

                    connection.Open();

                    SqlDataReader reader = commandSelect.ExecuteReader();

                    Employee currentEmp = new Employee();

                    if (!reader.HasRows)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No employee with the given ID: {id} found for update!");
                    }

                    reader.Read();
                    currentEmp.Id = reader.GetGuid(0);
                    currentEmp.FirstName = reader.GetString(1);
                    currentEmp.LastName = reader.GetString(2);
                    currentEmp.Birthday = reader.GetDateTime(3);

                    reader.Close();
                    SqlCommand commandUpdate = new SqlCommand("UPDATE EMPLOYEE SET FirstName=@firstName, LastName=@lastName, Birthday=@birthday WHERE Id=@Id;", connection);
                    commandUpdate.Parameters.AddWithValue("@Id", id);
                    commandUpdate.Parameters.AddWithValue("@firstName", string.IsNullOrWhiteSpace(employee.FirstName)? currentEmp.FirstName : employee.FirstName);
                    commandUpdate.Parameters.AddWithValue("@lastName", string.IsNullOrWhiteSpace(employee.LastName)? currentEmp.LastName : employee.LastName);
                    commandUpdate.Parameters.AddWithValue("@birthday", Convert.ToDateTime(string.IsNullOrWhiteSpace(employee.Birthday.ToString())? currentEmp.Birthday : employee.Birthday));

                    int numberOfAffectedRows = commandUpdate.ExecuteNonQuery();
                    if (numberOfAffectedRows > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, employee);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Error updating employee with an Id: {id}");
                    }
                }
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong, contact system admin!");
            }
        }
    
        // DELETE api/values/5
        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);

                using (connection)
                {
                    SqlCommand command = new SqlCommand("DELETE FROM EMPLOYEE WHERE Id=@Id", connection);
                    command.Parameters.AddWithValue("@Id", id);

                    int numberOfRowsAffected = command.ExecuteNonQuery();

                    if (numberOfRowsAffected > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Employee successfully deleted!");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Employee ID: {id} deletion failed!");
                    }
                }
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong, contact system admin!");
            }
        }
    }
}


