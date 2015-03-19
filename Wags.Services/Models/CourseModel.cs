namespace Wags.Services.Models
{
    using System.Collections.Generic;
    
    public class CourseModel:BaseModel
    {   
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CourseDataModel> CourseData { get; set; }
        public ClubModel Club { get; set; }
    }
}
