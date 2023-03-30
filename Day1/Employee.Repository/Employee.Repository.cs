using Employee.Common;
using Employee.Model;
using Employee.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        static string connectionString = "Data Source=DESKTOP-DG2UJNT;Initial Catalog=RentCar;Integrated Security=True";

        public async Task<List<EmployeeModel>> GetAllEmployeeAsync(Paging paging, Sorting sorting)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);

                using (connection)
                {
                    StringBuilder queryString = new StringBuilder();
                    queryString.AppendLine("SELECT * FROM EMPLOYEE ");

                    if (sorting != null)
                    {
                        queryString.AppendLine($"ORDER BY {sorting.OrderBy} ");
                    }

                    if (paging != null)
                    {
                        queryString.AppendLine("OFFSET (@pageNumber -1) * @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY;");
                    }

                    SqlCommand command = new SqlCommand(queryString.ToString(), connection);
                    command.Parameters.AddWithValue("@pageNumber", paging.PageNumber);
                    command.Parameters.AddWithValue("@pageSize", paging.PageSize);

                    //SqlCommand command = new SqlCommand("SELECT * FROM EMPLOYEE;", connection);

                    connection.Open();

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    List<EmployeeModel> employees = new List<EmployeeModel>();

                    while (reader.Read())
                    {
                        EmployeeModel emp = new EmployeeModel();

                        emp.Id = reader.GetGuid(0);
                        emp.FirstName = reader.GetString(1);
                        emp.LastName = reader.GetString(2);
                        emp.Birthday = reader.GetDateTime(3);

                        employees.Add(emp);
                    }



                    if (reader.HasRows)
                    {
                        reader.Close();
                        return employees;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<EmployeeModel> GetEmployeeAsync(Guid id)
        {

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);

                using (connection)
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM EMPLOYEE WHERE Id=@Id;", connection);

                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    EmployeeModel emp = new EmployeeModel();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        emp.Id = reader.GetGuid(0);
                        emp.FirstName = reader.GetString(1);
                        emp.LastName = reader.GetString(2);
                        emp.Birthday = reader.GetDateTime(3);

                        reader.Close();
                        return emp;
                    }

                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<bool> PostEmployeeAsync(EmployeeModel employee)
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

                    int numberOfRowsAffected = await command.ExecuteNonQueryAsync();

                    if (numberOfRowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<bool> PutEmployeeAsync(Guid id, EmployeeModel employee)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);

                using (connection)
                {
                    SqlCommand commandSelect = new SqlCommand("SELECT * FROM EMPLOYEE WHERE Id=@Id;", connection);

                    commandSelect.Parameters.AddWithValue("@id", id);

                    connection.Open();

                    SqlDataReader reader = await commandSelect.ExecuteReaderAsync();

                    EmployeeModel currentEmp = new EmployeeModel();

                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return true;
                    }

                    reader.Read();
                    currentEmp.Id = reader.GetGuid(0);
                    currentEmp.FirstName = reader.GetString(1);
                    currentEmp.LastName = reader.GetString(2);
                    currentEmp.Birthday = reader.GetDateTime(3);
                    reader.Close();

                    SqlCommand commandUpdate = new SqlCommand("UPDATE EMPLOYEE SET FirstName=@firstName, LastName=@lastName, Birthday=@birthday WHERE Id=@Id;", connection);
                    commandUpdate.Parameters.AddWithValue("@Id", id);
                    commandUpdate.Parameters.AddWithValue("@firstName", employee.FirstName);
                    commandUpdate.Parameters.AddWithValue("@lastName", employee.LastName);
                    commandUpdate.Parameters.AddWithValue("@birthday", employee.Birthday);

                    int numberOfAffectedRows = commandUpdate.ExecuteNonQuery();
                    if (numberOfAffectedRows > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);

                using (connection)
                {
                    SqlCommand command = new SqlCommand("DELETE FROM EMPLOYEE WHERE Id=@Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    int numberOfRowsAffected = await command.ExecuteNonQueryAsync();

                    if (numberOfRowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        }
    }
