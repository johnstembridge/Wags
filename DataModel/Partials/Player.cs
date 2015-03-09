using System;
using System.Linq;

namespace Wags.DataModel
{
    public partial class Player:IEntity
    {
        private History _currentStatus;
        public History CurrentStatus
        {
            get { return _currentStatus ?? StatusAtDate(DateTime.Today); }
        }

        public History StatusAtDate(DateTime date)
        {
            var current = Histories.OrderByDescending(h => h.Date).FirstOrDefault(h => h.Date <= date);
            if (current != null)
                current.Player = null;
            return current;
        }

        public void SetStatusAtDate(DateTime date)
        {
           var status = StatusAtDate(date);
           _currentStatus = status;
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
