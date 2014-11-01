using System;
using System.Linq;

namespace Wags.DataModel
{
    public partial class Player:IEntity
    {
        public History CurrentStatus
        {
            get { return Histories.OrderByDescending(h => h.Date).FirstOrDefault(); }
        }

        public History StatusAtDate(DateTime date)
        {
            return Histories.OrderByDescending(h => h.Date).FirstOrDefault(h => h.Date <= date);
        }

        public override string ToString()
        {
            return FullName;
        }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        public EntityState EntityState { get; set; }
    }
}
