using aspdotnetLabs.Models;
using aspdotnetLabs.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace aspdotnetLabs.Controllers;
[Authorize(Roles = "admin")]
public class BookController : Controller
{
    private readonly AppDbContext _context;
    
    public BookController(AppDbContext context)
    {
        _context = context;
    }
    
    private void InitSelect(BookEntity model)
    {
        model.Publishers =  _context
            .Publishers
            .Select(o => new SelectListItem() { Value = o.Id.ToString(), Text = o.Name })
            .ToList();
    }
    [AllowAnonymous]
   public IActionResult Index()
   {
       return View(_context.Books.ToList());
   }
    [HttpGet]
    public IActionResult Create()
    {
        BookEntity model = new BookEntity();
        InitSelect(model);
        return View(model);
    }

    [HttpPost]
    public ActionResult Create(BookEntity model)
    {
        if (ModelState.IsValid)
        {
            InitSelect(model);
            _context.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var model = _context.Books.FirstOrDefault(o => o.Id == id);
        if (model == null)
        {
            return NotFound();
        }
        InitSelect(model); // Populate the dropdown
        return View(model);
    }
    [HttpPost]
    public IActionResult Edit(BookEntity model)
    {
        if (ModelState.IsValid)
        {
            _context.Books.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            InitSelect(model);
            return View(_context.Books.FirstOrDefault(o => o.Id == model.Id));
        };
    }
    public IActionResult Details(int id)
    {
        if (_context.Books.Any(o => o.Id == id))
        {
            return View(_context.Books
                .Include(e => e.Publisher)
                .FirstOrDefault(o => o.Id == id));
        }
        else
        {
            return NotFound();
        };
    }

    public IActionResult Delete(int id)
    {
        if (_context.Books.Any(o => o.Id == id))
        {
            _context.Books.Remove(_context.Books.FirstOrDefault(o => o.Id == id));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return NotFound();
    }
}