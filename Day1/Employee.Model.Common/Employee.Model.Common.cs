using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Model.Common
{
    public interface IEmployeeModel
    {
        Guid Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime? Birthday { get; set; }
    }
}
