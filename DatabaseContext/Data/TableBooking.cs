//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseContext.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class TableBooking
    {
        public int Id { get; set; }
        public int Booking_id { get; set; }
        public int Table_id { get; set; }
        public bool Deleted { get; set; }
    
        public virtual Booking Booking { get; set; }
        public virtual Table Table { get; set; }
    }
}
