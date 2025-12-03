using Microsoft.AspNetCore.Mvc.Rendering;

namespace ari2._0.ViewModels;

public class PhoneCreateViewModel
{
    public Guid? ActorsId { get; set; }
    public Guid? PhoneTypesId { get; set; }
    public string? Number { get; set; }
    public string? Extension { get; set; }
    public bool? IsVerified { get; set; }
    public bool? IsEnabled { get; set; }
    
    public IEnumerable<SelectListItem> Actors { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> PhoneTypes { get; set; } = new List<SelectListItem>();
}
