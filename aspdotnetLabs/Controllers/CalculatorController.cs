using aspdotnetLabs.Models;
using Microsoft.AspNetCore.Mvc;

namespace aspdotnetLabs.Controllers;

public class CalculatorController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Result([FromForm]Calculator model)
    {
        if (!ModelState.IsValid)
        {
            return View("Error");
        }
        return View(model);
    }
}

