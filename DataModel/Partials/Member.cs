using System;
using System.Linq.Expressions;

namespace Wags.DataModel
{
    public partial class Member
    {
        public History CurrentStatus
        {
            get { return Player.CurrentStatus; }            
        }

        public History StatusAtDate(DateTime date)
        {
            return Player.StatusAtDate(date);
        }
    }
}
