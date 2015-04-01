namespace Wags.Services.Models
{
    public class PlayerModel:BaseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public StatusModel Status { get; set; }
    }
}
