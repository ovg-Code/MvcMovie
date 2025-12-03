using Microsoft.AspNetCore.Mvc;

namespace ari2._0.Controllers;

/// <summary>
/// Controlador MVC para la pagina principal.
/// </summary>
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
