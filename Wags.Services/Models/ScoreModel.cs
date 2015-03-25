namespace Wags.Services.Models
{
    public class ScoreModel:BaseModel
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string Player { get; internal set; }
        public StatusModel Status { get; internal set; }
        public int Position { get; set; }
        public int Points { get; set; }
        public int Shots { get; set; }
   }
}