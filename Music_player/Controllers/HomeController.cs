using Music_player.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Threading.Tasks;
using FileUploadDownload.Models;
using Music_player.Service;
using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace Music_player.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly ILogger<HomeController> _logger;

        private readonly IMusicService _musicService;

        private IHttpContextAccessor _accessor;
        public HomeController(ILogger<HomeController> logger, IMusicService musicService, IHttpContextAccessor accessor, IWebHostEnvironment webHost)
        {
            _webHostEnvironment = webHost;
            _logger = logger;
            _musicService = musicService;
            _accessor = accessor;
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

        public IActionResult Index()
        {
            List<string> Files = Directory.GetFiles(System.IO.Path.Combine(_webHostEnvironment.WebRootPath, "Upload")).ToList();
            FilesViewModel Model = new FilesViewModel();
            Model.Files = new List<FileDetails>();

            foreach (var file in Files) 
            {
                Model.Files.Add(new FileDetails {Path="data:audio/wav;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(file)), Name=Path.GetFileNameWithoutExtension(file)
            });
            }
            return View(Model);
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "Upload");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream stream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            FileDetails myModel = new FileDetails();
            myModel.Path = "data:audio/wav;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(path, file.FileName)));
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename is not availble");

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "/Upload", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(@path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
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
