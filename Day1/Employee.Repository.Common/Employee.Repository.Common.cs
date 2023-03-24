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
        Task<List<EmployeeModel>> GetAllEmployeeAsync();
        Task<EmployeeModel> GetEmployeeAsync(Guid id);
        Task<bool> PostEmployeeAsync(EmployeeModel employee);
        Task<bool> PutEmployeeAsync(Guid id, EmployeeModel employee);
        Task<bool> DeleteEmployeeAsync(Guid id);
    }
}
