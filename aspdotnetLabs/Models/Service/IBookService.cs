namespace aspdotnetLabs.Models.Service;

public interface IBookService
{
    int Add(Book book);
    void Delete(int id);
    void Update(Book book);
    List<Book> FindAll();
    Book? FindById(int id);
    bool Contains(int id);
    List<PublisherEntity> FindAllPublishersForVieModel();
}

