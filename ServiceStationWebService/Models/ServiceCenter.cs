//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceStationWebService.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ServiceCenter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceCenter()
        {
            this.BookingServices = new HashSet<BookingService>();
            this.DeliveryBoys = new HashSet<DeliveryBoy>();
            this.TypesOfServices = new HashSet<TypesOfService>();
        }
    
        public int ServiceCenterId { get; set; }
        public string ServiceCenterName { get; set; }
        public string Location { get; set; }
        public Nullable<decimal> Rating { get; set; }
        public Nullable<int> MaxNumberOfServices { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookingService> BookingServices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryBoy> DeliveryBoys { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TypesOfService> TypesOfServices { get; set; }
    }
}
