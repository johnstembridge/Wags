using System;

namespace Wags.DataModel
{
    public partial class Member:IEntity
    {
        public History CurrentStatus
        {
            get { return Player.CurrentStatus; }            
        }

        public History StatusAtDate(DateTime date)
        {
            return Player.StatusAtDate(date);
        }

        public EntityState EntityState { get; set; }
    }
}
