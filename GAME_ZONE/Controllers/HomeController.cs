using GAME_ZONE.Data;
using GAME_ZONE.Models;
using GAME_ZONE.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GAME_ZONE.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGamesService _GamesService;


        public HomeController( IGamesService gamesService)
        {
            _GamesService = gamesService;

        }

        

        public IActionResult Index()
        {
            var games = _GamesService.GetAll();
            return View(games);



        }


       



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
