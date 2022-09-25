namespace Core.Entities;

public abstract class BaseModel
{
    public long Id { get; set; }
    public Guid Hash { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
}