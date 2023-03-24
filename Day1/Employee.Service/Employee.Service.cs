using Employee.Model;
using Employee.Repository;
using Employee.Repository.Common;
using Employee.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Service
{
    public class EmployeeService : IEmployeeService
    {
        IEmployeeRepository _employeeRepository = new EmployeeRepository();

        public async Task<List<EmployeeModel>> GetAllEmployeeAsync()
        {
            List<EmployeeModel> employees = await _employeeRepository.GetAllEmployeeAsync();
            return employees;
        }

        public async Task<EmployeeModel> GetEmployeeAsync(Guid id)
        {
            EmployeeModel employeeModel =await _employeeRepository.GetEmployeeAsync(id);
            return employeeModel;
        }

        public async Task<bool> PostEmployeeAsync(EmployeeModel employee)
        {
            bool success = await _employeeRepository.PostEmployeeAsync(employee);
            return success;
        }

        public async Task<bool> PutEmployeeAsync(Guid id, EmployeeModel employee)
        {
            EmployeeModel currentEmp = await _employeeRepository.GetEmployeeAsync(id);
            if (currentEmp == null) 
            {
                return false;
            }

            EmployeeModel employeeToUpdate = new EmployeeModel
            {
                FirstName = employee.FirstName == default ? currentEmp.FirstName : employee.FirstName,
                LastName = employee.LastName == default ? currentEmp.LastName : employee.LastName,
                Birthday = employee.Birthday == default ? currentEmp.Birthday : employee.Birthday
            };

            bool success = await _employeeRepository.PutEmployeeAsync(id, employeeToUpdate);
            return success;
        }

        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            bool success = await _employeeRepository.DeleteEmployeeAsync(id);
            return success;
        }
    }
}
