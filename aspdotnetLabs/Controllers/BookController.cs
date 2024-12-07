using aspdotnetLabs.Models;
using Microsoft.AspNetCore.Mvc;

namespace aspdotnetLabs.Controllers;

public class BookController : Controller
{
    static Dictionary<int, Book> books = new Dictionary<int, Book>();
    public IActionResult Index()
    {
        return View(books);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Book model)
    {
        if (ModelState.IsValid)
        {
            int id = books.Keys.Count != 0 ? books.Keys.Max() : 0;
            model.Id = id + 1;
            books.Add(model.Id, model);
            return RedirectToAction("Index");
        }
        return View(model); // ponowne wyświetlenie formularza z informacjami o błędach
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        return View(books[id]);
    }
    [HttpPost]
    public IActionResult Edit(Book model)
    {
        if (ModelState.IsValid)
        {
            books[model.Id] = model;
            return RedirectToAction("Index");
        }
        else
        {
            return View(books[model.Id]);
        };
    }
    public IActionResult Details(int id)
    {
        if (books.Keys.Contains(id))
        {
            return View(books[id]);
        }
        else
        {
            return NotFound();
        };
    }

    public IActionResult Delete(int id)
    {
        books.Remove(id);
        return RedirectToAction("Index");
    }
}