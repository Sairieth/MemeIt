namespace MemeIt.Models.Common;

public abstract class EntityWithModificationDate : Entity
{
    public DateTime CreatedOn { get; set; }
    public DateTime LastModified { get; set; }
    public DateTime? DeletedOn { get; set; }
}