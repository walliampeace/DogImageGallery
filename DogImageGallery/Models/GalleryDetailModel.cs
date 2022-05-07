using System;
using System.Collections.Generic;

namespace DogImageGallery.Models
{
    public class GalleryDetailModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public string Url { get; set; }
        public List<string> Tags { get; set; }
    }
}
