namespace Wags.DataModel
{
    public partial class Booking:IEntity
    {
        public EntityState EntityState { get; set; }

        public override string ToString()
        {
            return Member.ToString() + " " + Event.ToString();
        }
    }
}
