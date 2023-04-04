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
using System.Web.ModelBinding;

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
            
                IQueryable<EMPLOYEE> query = Context.EMPLOYEE.AsQueryable();

                /*if (filtering != null)
                {
                    if (filtering.FirstName != null)
                    {
                        query = query.Where(e => e.FirstName == filtering.FirstName);
                    }

                    if (filtering.LastName != null)
                    {
                        query = query.Where(e => e.LastName == filtering.LastName);
                    }
                }

                if (sorting != null)
                {
                    if (sorting.OrderBy.Equals("Id"))
                    {
                        query = query.OrderBy(e => e.Id);
                    }
                    if (sorting.OrderBy.Equals("FirstName"))
                    {
                        query.OrderBy(e => e.FirstName);
                    }
                    if (sorting.OrderBy.Equals("LastName"))
                    {
                        query.OrderBy(e => e.LastName);
                    }
                } 

                if (paging != null && paging.PageNumber > 0)
                    {
                        int skipCount = (int)((paging.PageNumber - 1) * paging.PageSize);
                        query = query.Skip(skipCount).Take((int)paging.PageSize);
                    }*/

                List<EmployeeModel> employeeList = await Context.EMPLOYEE.Select(s => new EmployeeModel()
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                }).ToListAsync();

            return employeeList;
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
                EMPLOYEE empById = await Context.EMPLOYEE.FindAsync(id);

                EmployeeModel employee = new EmployeeModel
                {
                    Id = empById.Id,
                    FirstName = empById.FirstName,
                    LastName = empById.LastName
                };

                if (empById != null)
                {
                   return employee;
                }
                else
                {
                    return null;
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
                EMPLOYEE employeeDAL = new EMPLOYEE
                {
                    Id = Guid.NewGuid(),
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Birthday = employee.Birthday
                };

                if (employee != null)
                {
                    Context.EMPLOYEE.Add(employeeDAL);
                    await Context.SaveChangesAsync();
                }
                return true;
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
                EMPLOYEE employeeDAL = await Context.EMPLOYEE.FindAsync(id);
                if (employeeDAL == null)
                {
                    return false;
                }

                employeeDAL.FirstName = employee.FirstName;
                employeeDAL.LastName = employee.LastName;
                employeeDAL.Birthday = employee.Birthday;

                await Context.SaveChangesAsync();
                return true;
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
                EMPLOYEE employeeDAL = await Context.EMPLOYEE.FindAsync(id);
                if (employeeDAL == null)
                {
                    return false;
                }

                Context.EMPLOYEE.Remove(employeeDAL);
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
