using DogImageGallery.Models;
using Microsoft.AspNetCore.Mvc;

namespace DogImageGallery.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            var model = new GalleryIndexModel()
            {
               
            };
            return View(model);
        }
    }
}
