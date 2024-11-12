namespace GAME_ZONE.ViewModels
{
    public class CreateGameFormViewModel
    {


        [MaxLength(250)]
        [Required(ErrorMessage = "Please enter the name of the game.")]

        public string Name { get; set; } = string.Empty;







        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();

        [Display(Name = "Supported Devices")]
        public List<int> SelectedDevices { get; set; } = default!;

        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();
        [MaxLength(2500)]
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public IFormFile Cover { get; set; } = default!;


    }
}
