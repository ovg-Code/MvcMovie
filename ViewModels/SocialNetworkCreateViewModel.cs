using Microsoft.AspNetCore.Mvc.Rendering;

namespace ari2._0.ViewModels;

/// <summary>
/// ViewModel para el formulario de creación de redes sociales.
/// Sigue la mejor práctica de Microsoft: usar ViewModels en lugar de ViewBag/ViewData.
/// </summary>
public class SocialNetworkCreateViewModel
{
    public Guid? ActorsId { get; set; }
    public string? Platform { get; set; }
    public string? ProfileName { get; set; }
    public string? ProfileUrl { get; set; }
    public bool? IsEnabled { get; set; }
    
    public IEnumerable<SelectListItem> Actors { get; set; } = new List<SelectListItem>();
}
