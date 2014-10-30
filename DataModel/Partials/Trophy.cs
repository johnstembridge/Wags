using System;
using System.Configuration;

namespace Wags.DataModel
{
    public partial class Trophy : IEntity
    {
        public EntityState EntityState { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public Uri Image
        {
            get {
                var baseUri = ConfigurationManager.AppSettings["StaticBaseUri"];
                var uri = String.Format("{0}/images/trophies/{1}.jpg", baseUri, Name);
                return new Uri(uri); }
        }

        public Uri History {
            get {
                var baseUri = ConfigurationManager.AppSettings["ServiceBaseUri"];
                var uri = String.Format("{0}/trophies/{1}/history", baseUri, Id);
                return new Uri(uri); 
            } 
        }
    }
}