using Microsoft.AspNetCore.Mvc;

namespace ari2._0.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
