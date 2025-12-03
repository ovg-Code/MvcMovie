using Microsoft.AspNetCore.Mvc.Rendering;

namespace ari2._0.ViewModels;

/// <summary>
/// ViewModel para el formulario de creación de identificaciones.
/// Sigue la mejor práctica de Microsoft: usar ViewModels en lugar de ViewBag/ViewData.
/// </summary>
public class IdentityCardCreateViewModel
{
    public Guid ActorsId { get; set; }
    public Guid IdcardTypesId { get; set; }
    public string Idcard { get; set; } = null!;
    public bool? IsEnabled { get; set; }
    
    public IEnumerable<SelectListItem> Actors { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> IdcardTypes { get; set; } = new List<SelectListItem>();
}
