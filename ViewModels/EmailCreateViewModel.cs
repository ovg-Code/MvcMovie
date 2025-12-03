using Microsoft.AspNetCore.Mvc.Rendering;

namespace ari2._0.ViewModels;

public class EmailCreateViewModel
{
    public Guid ActorsId { get; set; }
    public string Definition { get; set; } = null!;
    public bool? IsNotification { get; set; }
    public bool? IsUnsuscribed { get; set; }
    public bool? IsPrimary { get; set; }
    public bool? IsEnabled { get; set; }
    
    public IEnumerable<SelectListItem> Actors { get; set; } = new List<SelectListItem>();
}
