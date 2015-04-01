namespace Wags.Services.Models
{  
    public class ClubModel:BaseModel
    {   
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Phone { get; set; }
        public string Directions { get; set; }   
        public Address Address { get; set; }
    }
}
