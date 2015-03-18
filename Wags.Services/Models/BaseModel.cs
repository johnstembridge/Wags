using System.Collections.Generic;

namespace Wags.Services.Models
{
    public class BaseModel
    {
        public IList<Link> Links { get; set; }
        public EntityState EntityState { get; set; }

        public void AddLink(string rel, string url)
        {
            if (this.Links == null)
                this.Links = new List<Link>();
            this.Links.Add(new Link{Rel=rel, Href=url});
        }
    }
}