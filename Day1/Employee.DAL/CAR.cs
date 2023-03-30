namespace Employee.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CAR")]
    public partial class CAR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAR()
        {
            DRIVER = new HashSet<DRIVER>();
            RENTAL = new HashSet<RENTAL>();
        }

        public Guid Id { get; set; }

        public Guid ModelId { get; set; }

        public decimal? RentPrice { get; set; }

        public int? Mileage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DRIVER> DRIVER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RENTAL> RENTAL { get; set; }

        public virtual MODEL MODEL { get; set; }
    }
}
