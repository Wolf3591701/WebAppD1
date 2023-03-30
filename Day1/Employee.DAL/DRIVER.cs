namespace Employee.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DRIVER")]
    public partial class DRIVER
    {
        public Guid Id { get; set; }

        public Guid CarId { get; set; }

        public Guid EmployeeId { get; set; }

        public virtual CAR CAR { get; set; }

        public virtual EMPLOYEE EMPLOYEE { get; set; }
    }
}
