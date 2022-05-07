using DagImageGallery.Data.Models;
using System.Collections.Generic;

namespace DogImageGallery.Models
{
    public class GalleryIndexModel
    {
        public IEnumerable<GalleryImage> Images { get; set; }
        public string SearchQuery { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
    }
}
