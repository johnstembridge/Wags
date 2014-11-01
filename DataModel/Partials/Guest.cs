namespace Wags.DataModel
{
    public partial class Guest : IEntity
    {
        public EntityState EntityState { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}