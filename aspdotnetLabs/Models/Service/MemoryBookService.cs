using aspdotnetLabs.Models.Service;

namespace aspdotnetLabs.Models.Service;
public class MemoryBookService : IBookService
{
    private Dictionary<int, Book> _books = new Dictionary<int, Book>();
    private IDateTimeProvider _timeProvider;

    public MemoryBookService(IDateTimeProvider dateTimeProvider)
    {
        _timeProvider = dateTimeProvider;
    }
    public int Add(Book book)
    {
        int id = _books.Keys.Count > 0 ? _books.Keys.Max() + 1 : 0;
        book.Id = id + 1;
        book.Created = _timeProvider.GetCurrentDateTime();
        _books.Add(book.Id, book);
        return book.Id;
    }

    public void Delete(int id)
    {
        _books.Remove(id);
    }

    public void Update(Book book)
    {
        book.Created = _books[book.Id].Created;
        _books[book.Id] = book;
    }

    public List<Book> FindAll()
    {
        return _books.Values.ToList();
    }

    public Book? FindById(int id)
    {
        return _books[id];
    }

    public bool Contains(int id)
    {
        return _books.ContainsKey(id);
    }
}