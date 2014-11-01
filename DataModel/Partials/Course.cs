namespace Wags.DataModel
{
    public partial class Course : IEntity
    {
        public EntityState EntityState { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
