using Microsoft.AspNetCore.Mvc.Rendering;

namespace ari2._0.ViewModels;

/// <summary>
/// ViewModel para el formulario de creación de relaciones entre actores.
/// Sigue la mejor práctica de Microsoft: usar ViewModels en lugar de ViewBag/ViewData.
/// </summary>
public class ActorRelationshipCreateViewModel
{
    public Guid ParentId { get; set; }
    public Guid ChildId { get; set; }
    public Guid RelationshipTypesId { get; set; }
    public bool? IsPercentage { get; set; }
    public bool? IsEnabled { get; set; }
    
    public IEnumerable<SelectListItem> ParentActors { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> ChildActors { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> RelationshipTypes { get; set; } = new List<SelectListItem>();
}
