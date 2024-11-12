using GAME_ZONE.Data;
using GAME_ZONE.Services;
using GAME_ZONE.ViewModels;
using GameZone.Models;
using Microsoft.AspNetCore.Mvc;

namespace GAME_ZONE.Controllers
{
    public class GamesController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IcategoriesServices _categoriesServices;
        private readonly IDevicesServices _DevicesServices;
        private readonly IGamesService _GamesService;
        public GamesController(ApplicationDbContext context, IcategoriesServices categoriesServices, IDevicesServices devicesServices, IGamesService gamesService)
        {
            _context = context;
            _categoriesServices = categoriesServices;
            _DevicesServices = devicesServices;
            _GamesService = gamesService;
        }
        public IActionResult Index()
        {
            var games = _GamesService.GetAll();
            return View(games);
           
        }

        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new()
            {

                Categories = _categoriesServices.GetSelectList(),


                Devices = _DevicesServices.GetSelectList()

            };
            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesServices.GetSelectList();

                model.Devices = _DevicesServices.GetSelectList();
                return View(model);
            };
           await _GamesService.Create(model);
            return RedirectToAction(nameof(Index));

        }



        public IActionResult Edit(int id)
        {
            var game = _GamesService.GetById(id);
            if (game is null)
                return NotFound();

            EditGameFormViewModel viewModel = new()
            {
                Id =game.Id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = _categoriesServices.GetSelectList(),
                Devices = _DevicesServices.GetSelectList(),
                currentCover = game.Cover,
            };

            return View(viewModel);
           
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesServices.GetSelectList();

                model.Devices = _DevicesServices.GetSelectList();
                return View(model);
            };

            var game = await _GamesService.Edit(model);

            if (game is null)
                return BadRequest();


            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(int id)
        {

            _GamesService.Delete(id);

            return RedirectToAction("Index");

        }

        public IActionResult Details(int id)
        {
            var game = _GamesService.GetById(id);

            return View(game);

        }

    }

}

