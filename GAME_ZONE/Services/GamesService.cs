using GAME_ZONE.Data;
using GAME_ZONE.ViewModels;
using GameZone.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GAME_ZONE.Services
{
    public class GamesService : IGamesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;

        public GamesService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "games");
        }
      

        public async Task Create(CreateGameFormViewModel model)
        

        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

           var coverName = await SaveCoverAsync(model.Cover);
         

            var game = new Game
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
               Cover = coverName,
                Devices = model.SelectedDevices?.Select(d => new GameDevice { DeviceId = d }).ToList() ?? new List<GameDevice>()
            };

            _context.Add(game);
            await _context.SaveChangesAsync();
        }

        public bool Delete(int id)
        {
            try
            {
                var game = _context.Games.Find(id); 
                if (game == null)
                {
                    return false; 
                }

                _context.Games.Remove(game);
                _context.SaveChanges(); 

                return true; 
            }
            catch
            {
                return false; 
            }
        }

        public async Task<Game?> Edit(EditGameFormViewModel model)
        {
            var game = await _context.Games
                            .Include(g => g.Devices) 
                            .FirstOrDefaultAsync(g => g.Id == model.Id);


            if (game is null)
                return null;
            
            var hasNewCover = model.Cover is not null;

            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;

            _context.GameDevices.RemoveRange(game.Devices);

            game.Devices = model.SelectedDevices.Select(d=> new GameDevice { DeviceId=d}).ToList();

            if (hasNewCover)
            {
                game.Cover = await SaveCoverAsync(model.Cover);
            }
            await _context.SaveChangesAsync();
            return game;
        }


 

        public IEnumerable<Game> GetAll()
        {
            
            
                var games = _context .Games.AsNoTracking().Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d =>d.Device)
                
                .ToList();
                return games;
         
            
        }

        public Game? GetById(int id)
        {


            var gamess = _context.Games.AsNoTracking().Include(g => g.Category)
            .Include(g => g.Devices)
            .ThenInclude(d => d.Device)

            .SingleOrDefault(g => g.Id == id);
            return gamess;
        }




        //دالة لاخذ الصورة من  الفورم و لحفظ الصورة 

        //   cover 
        //  دا اسم الفايل
        //  اللي بنتستقيل  فيه الصورة

        // دالة لحفظ الصورة المستلمة من النموذج
        private async Task<string> SaveCoverAsync(IFormFile cover)
        {
            if (cover == null || cover.Length == 0)
            {
                throw new ArgumentException("Cover image is required.");
            }
            // الامتداد بتاع الصورة
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
          

            



            //  الباث بتاع الصورة والامتداد
            var path = Path.Combine(_imagesPath, coverName);

          



            // إنشاء المجلد إذا لم يكن موجودًا
            Directory.CreateDirectory(_imagesPath);
            //حفظ الصورة داخل الباث 
            using var stream = new FileStream(path, FileMode.Create);
            await cover.CopyToAsync(stream);

            return coverName;
        }

    }
}
