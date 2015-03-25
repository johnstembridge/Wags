using System;
using System.Collections.Generic;

namespace Wags.Services.Models
{
    public class EventResultModel:BaseModel
    { 
        public int EventId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
    
        public ICollection<RoundModel> Rounds { get; set; }
   }
}