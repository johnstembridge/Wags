namespace Wags.Services.Models
{
    public class PlayerModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Handicap { get; set; }
        public StatusModel Status { get; set; }

        public EntityState EntityState { get; set; }
    }
}
