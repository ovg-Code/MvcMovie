using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var peliculas = new List<Movie>
        {
            new Movie { Id = 1, Title = "Cadena Perpetua", ReleaseDate = new DateTime(1994, 9, 23), Genre = "Drama", Price = 9.99M },
            new Movie { Id = 2, Title = "El Padrino", ReleaseDate = new DateTime(1972, 3, 24), Genre = "Crimen", Price = 12.99M },
            new Movie { Id = 3, Title = "El Caballero de la Noche", ReleaseDate = new DateTime(2008, 7, 18), Genre = "Acción", Price = 14.99M }
        };

        return View(peliculas);
    }
}
