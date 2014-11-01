namespace Wags.DataModel
{
    public partial class Score : IEntity
    {
        public EntityState EntityState { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", Position, Player.FullName);
        }
    }
}