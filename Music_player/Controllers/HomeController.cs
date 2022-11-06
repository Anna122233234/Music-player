using Music_player.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using FileUploadDownload.Models;
using Music_player.Service;

namespace Music_player.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IMusicService _musicService;

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

        public IActionResult Index1()
        {
            // Get files from the server
            var model = new FilesViewModel();
            foreach (var item in Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload")))
            {
                model.Files.Add(
                    new FileDetails { Name = System.IO.Path.GetFileName(item), Path = item });
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(IFormFile[] files)
        {
            // Iterate each files
            foreach (var file in files)
            {
                // Get the file name from the browser
                var fileName = System.IO.Path.GetFileName(file.FileName);

                // Get file path to be uploaded
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload", fileName);

                // Check If file with same name exists and delete it
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }


                // Create a new local file and copy contents of uploaded file
                using (var localFile = System.IO.File.OpenWrite(filePath))
                using (var uploadedFile = file.OpenReadStream())
                {
                    uploadedFile.CopyTo(localFile);
                }
            }
            ViewBag.Message = "Files are successfully uploaded";

            // Get files from the server
            var model = new FilesViewModel();
            foreach (var item in Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload")))
            {
                model.Files.Add(
                    new FileDetails { Name = System.IO.Path.GetFileName(item), Path = item });
            }
            return View(model);
        }

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename is not availble");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(@path, FileMode.Open))
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
