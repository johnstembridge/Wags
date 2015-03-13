namespace Wags.Services.Models
{
    /// <summary>
    /// Used to allow Client to set the state of entities to allow server to resolve updates
    /// </summary>
    public interface IEntity
    {
        EntityState EntityState { get; set; }
    }

    public enum EntityState
    {
        Unchanged,
        Added,
        Modified,
        Deleted
    }
}
