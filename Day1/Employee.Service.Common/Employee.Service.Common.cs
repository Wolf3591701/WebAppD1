using Employee.Common;
using Employee.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Service.Common
{
    public interface IEmployeeService
    {
        Task<List<EmployeeModel>> GetAllEmployeeAsync(Paging paging, Sorting sorting);
        Task<EmployeeModel> GetEmployeeAsync(Guid id);
        Task<bool> PostEmployeeAsync(EmployeeModel employee);
        Task<bool> PutEmployeeAsync(Guid id, EmployeeModel employee);
        Task<bool> DeleteEmployeeAsync(Guid id);
    }
}
