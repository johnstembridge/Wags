namespace Wags.Services.Models
{
    using System;

    public class RoundModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }    
        public int CourseId { get; set; }
        public CourseModel Course { get; set; }

        public EntityState EntityState { get; set; }
    }
}
