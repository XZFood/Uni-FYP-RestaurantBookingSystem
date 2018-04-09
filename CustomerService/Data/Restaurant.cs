namespace CustomerService.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Restaurant")]
    public partial class Restaurant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Restaurant()
        {
            Bookings = new HashSet<Booking>();
            RestaurantMenuItems = new HashSet<RestaurantMenuItem>();
            Tables = new HashSet<Table>();
        }

        public int id { get; set; }

        public int company_id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [Required]
        [StringLength(15)]
        public string phoneNo { get; set; }

        [Required]
        [StringLength(100)]
        public string addressStreet { get; set; }

        [Required]
        [StringLength(50)]
        public string addressCounty { get; set; }

        [Required]
        [StringLength(50)]
        public string addressTown { get; set; }

        [Required]
        [StringLength(8)]
        public string addressPostalCode { get; set; }

        public bool deleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual Company Company { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RestaurantMenuItem> RestaurantMenuItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Table> Tables { get; set; }
    }
}
