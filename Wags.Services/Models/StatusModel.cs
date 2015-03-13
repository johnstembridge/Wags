namespace Wags.Services.Models
{
    public class StatusModel
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public PlayerStatus Status { get; set; }
        public decimal Handicap { get; set; }
    }
}
