using Microsoft.AspNetCore.Mvc.Rendering;

namespace ari2._0.ViewModels;

/// <summary>
/// ViewModel para el formulario de creación de direcciones.
/// Sigue la mejor práctica de Microsoft: usar ViewModels en lugar de ViewBag/ViewData.
/// </summary>
public class AddressCreateViewModel
{
    public Guid? ActorsId { get; set; }
    public Guid? AddressTypesId { get; set; }
    public Guid? ZipCodesId { get; set; }
    public string? Street { get; set; }
    public string? Apartment { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public bool? IsVerified { get; set; }
    public bool? IsEnabled { get; set; }
    
    public IEnumerable<SelectListItem> Actors { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> AddressTypes { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> ZipCodes { get; set; } = new List<SelectListItem>();
}
