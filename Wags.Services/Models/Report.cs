using System.Collections.Generic;

namespace Wags.Services.Models
{
    public class Report
    {
        public string Title { get; set; }

        public string[] Headings { get; set; }

        public List<object[]> Data { get; set; }
    }
}
