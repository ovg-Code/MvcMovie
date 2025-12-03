using Microsoft.AspNetCore.Mvc.Rendering;

namespace ari2._0.ViewModels;

/// <summary>
/// ViewModel para el formulario de creación de clientes.
/// Sigue la mejor práctica de Microsoft: usar ViewModels en lugar de ViewBag/ViewData
/// para poblar dropdowns con SelectListItem.
/// Referencia: https://learn.microsoft.com/en-us/aspnet/core/mvc/views/working-with-forms
/// </summary>
public class CustomerCreateViewModel
{
    // Propiedades del modelo Customer
    public Guid? ActorsId { get; set; }
    public Guid? CustomerPublicStatusTypesId { get; set; }
    public bool? IsAgentRetention { get; set; }
    public bool? IsLeasing { get; set; }
    public bool? IsEnabled { get; set; }
    public string? OtherData { get; set; }
    
    // Listas para poblar los dropdowns (mejor práctica vs ViewBag)
    public IEnumerable<SelectListItem> Actors { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> CustomerPublicStatusTypes { get; set; } = new List<SelectListItem>();
}
