using DagImageGallery.Data;
using DogImageGallery.Models;
using DogImageGallery.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
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
        public IActionResult Search()
        {
            var model = new SearchImageModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SearchImage(IFormFile file, string tags, string title)
        {
            var container = _imageService.GetBlobContainer(_azureConnectionString, "images");
            List<BlobInfo> blobs = new List<BlobInfo>();
            /*
            foreach (IListBlobItem item in container.ListBlobsSegmentedAsync)
            {
                var blob = item as CloudBlockBlob;

                if (blob != null)
                {
                    blobs.Add(new BlobInfo()
                    {
                        ImageUri = blob.Uri.ToString(),
                        ThumbnailUri = blob.Uri.ToString().Replace("/photos/", "/thumbnails/")
                    });
                }
            }

            ViewBag.Blobs = blobs.ToArray();
            return View();

            // Process with Cognitive Vision API
            /* 
             * ComputerVisionClient computerVision = new ComputerVisionClient(
                    new ApiKeyServiceClientCredentials("https://dogsclassifier.cognitiveservices.azure.com/customvision/v3.0/Prediction/56cd63be-e7e4-4e11-a798-1fb82d7257db/classify/iterations/classifyDogs/url"),
                    new System.Net.Http.DelegatingHandler[] { });

            return RedirectToAction("Index", "Gallery");
            */
            return RedirectToAction("Index", "Gallery");
        }
    }
}
