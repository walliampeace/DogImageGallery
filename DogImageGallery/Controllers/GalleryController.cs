using DagImageGallery.Data;
using DagImageGallery.Data.Models;
using DogImageGallery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace DogImageGallery.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IImage _imageService;
        private readonly ILogger<GalleryController> _logger;
        public GalleryController(IImage imageService, ILogger<GalleryController> logger)
        {
            _imageService = imageService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var imageList = _imageService.GetAll();
            var model = new GalleryIndexModel()
            {
                Images = imageList,
                SearchQuery = ""
            };
            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var image = _imageService.GetById(id);

            var model = new GalleryDetailModel()
            {
                Id = image.Id,
                Title = image.Title,
                Created = image.Created,
                Url = image.Url,
                Tags = image.Tags
                    .Select(t => t.Description)
                    .ToList()
            };

            return View(model);
        }
        // Edit
        // Get
        public IActionResult Edit(int id)
        {
            var imageToEdit = _imageService.GetById(id);

            return View(imageToEdit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, GalleryImage changeImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                _imageService.UpdateImage(changeImage);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return View(changeImage);
            }

        }
        // Delete
        // Get
        public IActionResult Delete(int id)
        {
            var imageToDelete = _imageService.GetById(id);

            return View(imageToDelete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(GalleryImage image)
        {
            if (image == null)
            {
                return NotFound();
            }

            try
            {
                _imageService.DeleteImage(image.Id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return View(_imageService.GetById(image.Id));
            }
        }
    }
}
