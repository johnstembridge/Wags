//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wags.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Guest:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Handicap { get; set; }
        public Nullable<int> BookingId { get; set; }
    
        public virtual Booking Booking { get; set; }
    }
}
