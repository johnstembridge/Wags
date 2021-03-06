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
    
    public partial class Member : Player
    {
        public Member()
        {
            this.Transactions = new HashSet<Transaction>();
            this.Bookings = new HashSet<Booking>();
            this.Events = new HashSet<Event>();
            this.Address = new Address();
        }
    
        public string Email { get; set; }
        public string Phone { get; set; }
    
        public Address Address { get; set; }
    
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
