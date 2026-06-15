using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace BookNest.Controllers;
[Area("Customer")]
public class HomeController : Controller {
    public IActionResult Index() {
        return View();
    }

    public IActionResult Privacy() {
        return View();
    }

   
}