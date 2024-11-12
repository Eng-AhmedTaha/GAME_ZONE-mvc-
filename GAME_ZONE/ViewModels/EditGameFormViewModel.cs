namespace GAME_ZONE.ViewModels
{
    public class EditGameFormViewModel
    {

        public int Id { get; set; }

        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public string? currentCover { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();

        [Display(Name = "Supported Devices")]
        public List<int> SelectedDevices { get; set; } = default!;

        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();
        [MaxLength(2500)]

        public string Description { get; set; } = string.Empty;
        public IFormFile? Cover { get; set; } = default!;


    }
}
