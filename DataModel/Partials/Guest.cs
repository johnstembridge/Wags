namespace Wags.DataModel
{
    public partial class Guest : Player, IEntity
    {
        public override string ToString()
        {
            return Name;
        }
    }
}