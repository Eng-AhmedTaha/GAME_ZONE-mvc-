using GAME_ZONE.ViewModels;

namespace GAME_ZONE.Services
{
    public interface IGamesService
    {
        Task Create(CreateGameFormViewModel model);
        Task<Game?> Edit(EditGameFormViewModel model);
        IEnumerable<Game> GetAll();

        Game? GetById(int id);
        public bool Delete(int id);

    }
}
