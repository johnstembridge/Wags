using System;
using System.Linq;

namespace Wags.DataModel
{
    public partial class Player
    {
        public History CurrentStatus
        {
            get { return Histories.OrderBy(h => h.Date).Last(); }

        }

        public History StatusAtDate(DateTime date)
        {
            return Histories.OrderBy(h => h.Date).Last(h => h.Date < date);
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }
    }
}
