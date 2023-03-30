namespace Employee.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RENTAL")]
    public partial class RENTAL
    {
        public Guid Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Guid CarId { get; set; }

        public Guid CustomerId { get; set; }

        public virtual CAR CAR { get; set; }

        public virtual CUSTOMER CUSTOMER { get; set; }
    }
}
