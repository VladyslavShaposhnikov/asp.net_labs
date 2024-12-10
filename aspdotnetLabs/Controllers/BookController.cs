using aspdotnetLabs.Models;
using aspdotnetLabs.Models.Service;
using Microsoft.AspNetCore.Mvc;

namespace aspdotnetLabs.Controllers;

public class BookController : Controller
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }
   public IActionResult Index()
    {
        return View(_bookService.FindAll());
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
            _bookService.Add(model);
            return RedirectToAction("Index");
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        return View(_bookService.FindById(id));
    }
    [HttpPost]
    public IActionResult Edit(Book model)
    {
        if (ModelState.IsValid)
        {
            _bookService.Update(model);
            return RedirectToAction("Index");
        }
        else
        {
            return View(_bookService.FindById(model.Id));
        };
    }
    public IActionResult Details(int id)
    {
        if (_bookService.Contains(id))
        {
            return View(_bookService.FindById(id));
        }
        else
        {
            return NotFound();
        };
    }

    public IActionResult Delete(int id)
    {
        if (_bookService.Contains(id))
        {
            _bookService.Delete(id);
            return RedirectToAction("Index");
        }
        return NotFound();
    }
}