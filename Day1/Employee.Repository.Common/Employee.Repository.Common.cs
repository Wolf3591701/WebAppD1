using Employee.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Repository.Common
{
    public interface IEmployeeRepository
    {
        List<EmployeeModel> GetAllEmployee();
        EmployeeModel GetEmployee(Guid id);
        bool PostEmployee(EmployeeModel employee);
        bool PutEmployee(Guid id, EmployeeModel employee);
        bool DeleteEmployee(Guid id);
    }
}
