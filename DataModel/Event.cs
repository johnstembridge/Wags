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
    
    public partial class Event
    {
        public Event()
        {
            this.Bookings = new HashSet<Booking>();
            this.Rounds = new HashSet<Round>();
            this.Organisers = new HashSet<Organiser>();
        }
    
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> MemberPrice { get; set; }
        public Nullable<decimal> GuestPrice { get; set; }
        public Nullable<decimal> DinnerPrice { get; set; }
        public Nullable<System.DateTime> BookingDeadline { get; set; }
        public Nullable<int> MaxPlayers { get; set; }
        public string Schedule { get; set; }
        public string Notes { get; set; }
        public string Url { get; set; }
    
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Round> Rounds { get; set; }
        public virtual Trophy Trophy { get; set; }
        public virtual ICollection<Organiser> Organisers { get; set; }
    }
}
