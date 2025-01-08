namespace aspdotnetLabs.Models;

public class PublisherEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Regon { get; set; }
    public string Nip { get; set; }
    public Address? Address { get; set; }
    public ISet<BookEntity> Book { get; set;}
}