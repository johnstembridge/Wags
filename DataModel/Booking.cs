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
    
    public partial class Booking
    {
        public Booking()
        {
            this.Guests = new HashSet<Guest>();
        }
    
        public int Id { get; set; }
        public System.DateTime Timestamp { get; set; }
        public bool Attending { get; set; }
        public string Comment { get; set; }
        public int EventId { get; set; }
        public int MemberId { get; set; }
    
        public virtual Event Event { get; set; }
        public virtual Member Member { get; set; }
        public virtual ICollection<Guest> Guests { get; set; }
    }
}
