//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarServicingManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DeliveryBoy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DeliveryBoy()
        {
            this.BookingServices = new HashSet<BookingService>();
        }
    
        public int DeliveryBoyId { get; set; }
        public string Name { get; set; }
        public string Experience { get; set; }
        public Nullable<int> ServiceCenterId { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<int> Mobile { get; set; }
        public string Address { get; set; }
    
        public virtual ServiceCenter ServiceCenter { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookingService> BookingServices { get; set; }
    }
}
