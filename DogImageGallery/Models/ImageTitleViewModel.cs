using DagImageGallery.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DogImageGallery.Models
{
    public class ImageTitleViewModel
    {
        public List<GalleryImage> Images;
        public SelectList Title;
        public string ImageTitle { get; set; }
    }
}
