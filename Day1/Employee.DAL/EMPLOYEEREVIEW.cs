namespace Employee.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EMPLOYEEREVIEW")]
    public partial class EMPLOYEEREVIEW
    {
        public Guid Id { get; set; }

        public Guid? EmployeeId { get; set; }

        public DateTime? ReviewDate { get; set; }

        public string Review { get; set; }

        public bool? ReviewApproved { get; set; }

        public virtual EMPLOYEE EMPLOYEE { get; set; }
    }
}
