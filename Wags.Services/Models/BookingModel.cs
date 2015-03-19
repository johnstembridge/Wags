namespace Wags.Services.Models
{
    using System;
    using System.Collections.Generic;

    public class BookingModel:BaseModel
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Attending { get; set; }
        public string Comment { get; set; }
        public EventModel Event { get; set; }
        public MemberModel Member { get; set; }
        public ICollection<GuestModel> Guests { get; set; }
    }
}
