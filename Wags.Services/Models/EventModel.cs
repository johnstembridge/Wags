using System;
using System.Collections.Generic;

namespace Wags.Services.Models
{
    public class EventModel
    { 
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public decimal? MemberPrice { get; set; }
        public decimal? GuestPrice { get; set; }
        public decimal? DinnerPrice { get; set; }
        public DateTime? BookingDeadline { get; set; }
        public int? MaxPlayers { get; set; }
        public string Schedule { get; set; }
        public string Notes { get; set; }
        public string Url { get; set; }
    
        public ICollection<RoundModel> Rounds { get; set; }
        public TrophyModel Trophy { get; set; }
        public ICollection<MemberModel> Organisers { get; set; }

        public EntityState EntityState { get; set; }
   }
}