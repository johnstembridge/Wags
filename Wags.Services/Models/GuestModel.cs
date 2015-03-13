namespace Wags.Services.Models
{
    public class GuestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Handicap { get; set; }

        public EntityState EntityState { get; set; }
    }
}
