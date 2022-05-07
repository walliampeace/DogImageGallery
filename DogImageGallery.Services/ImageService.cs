using DagImageGallery.Data;
using DagImageGallery.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DogImageGallery.Services
{
    public class ImageService : IImage
    {
        private readonly DogImageGalleryDbContext _ctx;
        public ImageService(DogImageGalleryDbContext ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<GalleryImage> GetAll()
        {
            return _ctx.DogImages
                .Include(img => img.Tags);
        }
        public GalleryImage GetById(int id)
        {
            return GetAll()
            .Where(i => i.Id == id)
            .FirstOrDefault();
        }
        public IEnumerable<GalleryImage> GetWithTag(string tag)
        {
            return GetAll().Where(img
                => img.Tags
                .Any(t => t.Description == tag));
        }
    }
}
