﻿namespace DogImageGallery.Models
{
    public class SearchImageModel
    {
        public string Title { get; set; }
        public string Tags { get; set; } // add logic to parse multiple user tags in the views
        // public IFormFile ImageUpload { get; set; } // IFormFile needs Http services.
    }
}
