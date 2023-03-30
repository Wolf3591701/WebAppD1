namespace Employee.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CUSTOMERINFO")]
    public partial class CUSTOMERINFO
    {
        public Guid Id { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(25)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public virtual CUSTOMER CUSTOMER { get; set; }
    }
}
