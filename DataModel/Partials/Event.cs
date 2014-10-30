using System;
using System.Configuration;

namespace Wags.DataModel
{
    public partial class Event : IEntity
    {
        public EntityState EntityState { get; set; }

        public override string ToString()
        {
            return Name + " " + Date.Date.ToShortDateString();
        }

        public Uri Result
        {
            get
            {
                var baseUri = ConfigurationManager.AppSettings["ServiceBaseUri"];
                var uri = String.Format("{0}/Events/Result/{1}", baseUri, Id);
                return new Uri(uri);
            }
        }

        public Uri Report
        {
            get
            {
                var baseUri = ConfigurationManager.AppSettings["StaticBaseUri"];
                var uri = String.Format("{0}/events/{1}/{2}", baseUri, Date.Year, ReportFileName);
                return new Uri(uri);
            }
        }

        private string ReportFileName
        {
            get { return String.Format("rp{0}.htm", Date.ToString("yyMMdd")); }
        }

    }
}