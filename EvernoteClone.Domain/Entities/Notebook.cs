namespace EvernoteClone.Domain.Entities;

public class Notebook : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public ICollection<Note> Notes { get; set; } = [];
}
