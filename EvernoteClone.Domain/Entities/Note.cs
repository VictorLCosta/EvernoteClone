namespace EvernoteClone.Domain.Entities;

public class Note : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string FileLocation { get; set; } = string.Empty;

    public int NotebookId { get; set; }
    public Notebook Notebook { get; set; } = null!;
}
