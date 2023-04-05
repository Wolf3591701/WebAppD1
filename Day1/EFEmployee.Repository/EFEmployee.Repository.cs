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

                if (!string.IsNullOrEmpty(filtering.SearchString))
                {
                    query = query.Where(e => e.FirstName.Contains(filtering.SearchString) || e.LastName.Contains(filtering.SearchString));
                }

                if (sorting != null)
                {
                    switch (sorting.SortOrder)
                    {
                        case "FirstName desc":
                            query = query.OrderByDescending(e => e.FirstName); 
                            break;
                        case "FirstName":
                            query = query.OrderBy(e => e.FirstName); 
                            break;
                        case "LastName desc":
                            query = query.OrderByDescending(e => e.LastName);
                            break;
                        case "LastName":
                            query = query.OrderBy(e => e.LastName);
                            break;
                        case "Birthday desc":
                            query = query.OrderByDescending(e => e.Birthday);
                            break;
                        case "Birthday":
                            query = query.OrderBy(e => e.Birthday);
                            break;
                        default:
                            query = query.OrderBy(e => e.LastName);
                            break;
                    }
                } 

                /*if (paging != null && paging.PageNumber > 0)
                    {
                        int skipCount = (int)((paging.PageNumber - 1) * paging.PageSize);
                        query = query.Skip(skipCount).Take((int)paging.PageSize);
                    }*/

                List<EmployeeModel> employeeList = await query.Select(s => new EmployeeModel()
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Birthday = s.Birthday
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
                EMPLOYEE emp = await Context.EMPLOYEE.FindAsync(id);

                EmployeeModel employee = new EmployeeModel
                {
                    Id = emp.Id,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    Birthday = emp.Birthday
                };

                if (emp != null)
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
