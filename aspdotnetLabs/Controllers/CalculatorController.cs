using Microsoft.AspNetCore.Mvc;

namespace aspdotnetLabs.Controllers;

public class CalculatorController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Result(Operation? op, double? number1, double? number2)
    {
        if (number1 == null || number2 == null || op == null)
        {
            ViewBag.Result = "Invalid input. Please ensure all fields are filled correctly.";
            return View();
        }
        ViewBag.Op = op;
        ViewBag.num1 = number1;
        ViewBag.num2 = number2;
        switch (op)
        {
            case Operation.Add:
                ViewBag.Result = number1 + number2;
                break;
            case Operation.Sub:
                ViewBag.Result = number1 - number2;
                break;
            case Operation.Mul:
                ViewBag.Result = number1 * number2;
                break;
            case Operation.Div:
                if (number1 == 0)
                {
                    ViewBag.result = "You cannot divide by zero.";
                    return View();
                }
                else
                {
                    @ViewBag.Result = number1 / number2;
                }
                break;
            default:
                @ViewBag.Result = "Something went wrong. Unknown operation.";
                break;
        }
        return View();
    }
}

public enum Operation
{
    Add,    
    Sub,
    Mul,
    Div
}