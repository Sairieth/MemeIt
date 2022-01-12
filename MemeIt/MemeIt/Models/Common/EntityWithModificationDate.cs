namespace MemeIt.Models.Common;

public abstract class EntityWithModificationDate
{
    public DateTime CreatedOn { get; set; }
    public DateTime LastModified { get; set; }
    public DateTime DeletedOn { get; set; }
}