using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Music_player.Service;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Music_player.Models;
using System;

namespace Backup_Service.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        private readonly IMusicService _musicService;

        public object model { get; private set; }

        public HomeController(ILogger<HomeController> logger, IMusicService musicService)
        {
            _logger = logger;
            _musicService = musicService;
        }

        public IActionResult Index()
        {
            return View(_musicService.GetListMusic());
        }
        public JsonResult MusicList()
        {
            return Json(_musicService.GetListMusic());
        }
            [HttpPost]
        
        private IActionResult View(Func<object> getList)
        {
            throw new NotImplementedException();
        }

        private object GetList()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Add(string filename)
        {
            if (filename == null)
                return Content("filename is not availble");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".mp3", "music/mp3"}
            };
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}