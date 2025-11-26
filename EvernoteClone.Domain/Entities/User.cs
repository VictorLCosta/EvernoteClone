namespace EvernoteClone.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public ICollection<Notebook> Notebooks { get; set; } = [];
}
