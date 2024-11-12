using GAME_ZONE.Data;

namespace GAME_ZONE.Services
{
    public class categoriesServices : IcategoriesServices
    {
        private readonly ApplicationDbContext _context;

        public categoriesServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetSelectList()
        {
           return _context.Categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).OrderBy(c => c.Text)
              .AsNoTracking()

                  .ToList();

        }
    }
}
