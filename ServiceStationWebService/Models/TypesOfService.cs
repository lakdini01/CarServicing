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
    
    public partial class TypesOfService
    {
        public int ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }
        public Nullable<double> Cost { get; set; }
        public Nullable<int> CenterId { get; set; }
    
        public virtual ServiceCenter ServiceCenter { get; set; }
    }
}
