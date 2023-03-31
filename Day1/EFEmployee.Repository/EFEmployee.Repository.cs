using Employee.Common;
using Employee.DAL;
using Employee.Model;
using Employee.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFEmployee.Repository
{
    public class EFEmployeeRepository : IEmployeeRepository
    {
       private RentCarContext Context { get; set; }
        public EFEmployeeRepository(RentCarContext context)
        {
            Context = context;
        }

       

        public async Task<List<EmployeeModel>> GetAllEmployeeAsync(Paging paging, Sorting sorting, Filtering filtering)
        {
            try
            {
               // IList<EMPLOYEE> employeeDto;
                var employeeList = await Context.EMPLOYEE.Select(s => new EmployeeModel()
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                }).ToListAsync();

                if (employeeList.Count == 0)
                {
                    return null;
                }
                return employeeList;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<EmployeeModel> GetEmployeeAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PostEmployeeAsync(EmployeeModel employee)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PutEmployeeAsync(Guid id, EmployeeModel employee)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEmployeeAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
