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
    
    public partial class RestaurantImage
    {
        public int Id { get; set; }
        public int Restaurant_id { get; set; }
        public int Image_id { get; set; }
        public bool Deleted { get; set; }
    
        public virtual Image Image { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}