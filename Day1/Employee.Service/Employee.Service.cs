using Employee.Common;
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
        //IEmployeeRepository _employeeRepository = new EmployeeRepository();
        protected IEmployeeRepository EmployeeRepository { get; set; }

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            EmployeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeModel>> GetAllEmployeeAsync(Paging paging, Sorting sorting, Filtering filtering)
        {
            List<EmployeeModel> employees = await EmployeeRepository.GetAllEmployeeAsync(paging, sorting, filtering);
            return employees;
        }

        public async Task<EmployeeModel> GetEmployeeAsync(Guid id)
        {
            EmployeeModel employeeModel = await EmployeeRepository.GetEmployeeAsync(id);
            return employeeModel;
        }

        public async Task<bool> PostEmployeeAsync(EmployeeModel employee)
        {
            bool success = await EmployeeRepository.PostEmployeeAsync(employee);
            return success;
        }

        public async Task<bool> PutEmployeeAsync(Guid id, EmployeeModel employee)
        {
            EmployeeModel currentEmp = await EmployeeRepository.GetEmployeeAsync(id);
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

            bool success = await EmployeeRepository.PutEmployeeAsync(id, employeeToUpdate);
            return success;
        }

        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            bool success = await EmployeeRepository.DeleteEmployeeAsync(id);
            return success;
        }
    }
}
