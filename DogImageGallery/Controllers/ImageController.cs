using DagImageGallery.Data;
using DogImageGallery.Models;
using DogImageGallery.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DogImageGallery.Controllers
{
    public class ImageController : Controller
    {
        private readonly IConfiguration _config;
        private IImage _imageService;
        private string _azureConnectionString { get; }
        public ImageController(IConfiguration config, IImage imageService)
        {
            _config = config;
            _azureConnectionString = _config["AzureStorageConnectionString"];
            _imageService = imageService;
        }


        public IActionResult Upload()
        {
            var model = new UploadImageModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UploadNewImage(IFormFile file, string tags, string title)
        {
            var container = _imageService.GetBlobContainer(_azureConnectionString, "images");

            var content = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
            var fileName = content.FileName.Trim('"');

            // Get a refenrence to a Block Blob
            var blockBlob = container.GetBlockBlobReference(fileName);

            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());
            await _imageService.SetImage(title, tags, blockBlob.Uri);

            return RedirectToAction("Index", "Gallery");
        }
    }
}
