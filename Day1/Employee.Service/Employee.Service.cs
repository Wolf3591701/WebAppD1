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

        public List<EmployeeModel> GetAllEmployee()
        {
            List<EmployeeModel> employees = _employeeRepository.GetAllEmployee();
            return employees;
        }

        public EmployeeModel GetEmployee(Guid id)
        {
            EmployeeModel employeeModel = _employeeRepository.GetEmployee(id);
            return employeeModel;
        }

        public bool PostEmployee(EmployeeModel employee)
        {
            bool success = _employeeRepository.PostEmployee(employee);
            return success;
        }

        public bool PutEmployee(Guid id, EmployeeModel employee)
        {
            EmployeeModel currentEmp = _employeeRepository.GetEmployee(id);
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

            bool success = _employeeRepository.PutEmployee(id, employeeToUpdate);
            return success;
        }

        public bool DeleteEmployee(Guid id)
        {
            bool success = _employeeRepository.DeleteEmployee(id);
            return success;
        }
    }
}
