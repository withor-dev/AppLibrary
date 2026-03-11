namespace LibraryAPI.Domain.Entities;

public class Author : Entity
{
    public string Name { get; private set; }
    public ICollection<Book> Books { get; private set; }

    public Author(string name)
    {
        UpdateName(name);
        Books = new List<Book>();
    }

    public void UpdateName(string name)
    {
        if (name == null || string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome do autor é obrigatório.");
            
        Name = name;
    }
}