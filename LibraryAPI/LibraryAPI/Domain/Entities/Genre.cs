namespace LibraryAPI.Domain.Entities;

public class Genre : Entity
{
    public string Name { get; private set; }
    
    public ICollection<Book> Books { get; private set; }

    public Genre(string name)
    {
        UpdateName(name);
        Books = new List<Book>();
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome do gênero é obrigatório.");
            
        Name = name;
    }
}