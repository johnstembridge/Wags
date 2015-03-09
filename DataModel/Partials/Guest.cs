namespace Wags.DataModel
{
    public partial class Guest : IEntity
    {
        public EntityState EntityState { get; set; }

        public string FirstName()
        {
            var names = Name.Split(' ');
            return names[0];
        }

        public string LastName()
        {
            var names = Name.Split(' ');
            return names[names.Length - 1];
        }

        public override string ToString()
        {
            return Name;
        }
    }
}