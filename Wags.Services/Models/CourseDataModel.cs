namespace Wags.Services.Models
{
    using System;
    
    public class CourseDataModel:BaseModel
    {
        public int Id { get; set; }
        public DateTime EffectiveDate { get; set; }
        public int? SSS { get; set; }
        public int Par { get; set; }
    }
}
