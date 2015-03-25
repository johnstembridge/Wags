using System;
using System.Collections.Generic;

namespace Wags.Services.Models
{
    public class RoundModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }    
        public CourseModel Course { get; set; }
        public ICollection<ScoreModel> Scores { get; set; } 
        public EntityState EntityState { get; set; }
    }
}
