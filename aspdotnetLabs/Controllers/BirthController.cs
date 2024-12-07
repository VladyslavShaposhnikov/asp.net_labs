using aspdotnetLabs.Models;
using Microsoft.AspNetCore.Mvc;

namespace aspdotnetLabs.Controllers;

public class BirthController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Result(Birth model)
    {
        if (!model.isValid())
        {
            return View("/Views/Shared/Error");
        }
        return View(model);
    }
}