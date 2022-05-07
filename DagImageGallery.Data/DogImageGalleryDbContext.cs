using DagImageGallery.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DagImageGallery.Data
{
    public class DogImageGalleryDbContext : DbContext
    {
        public DogImageGalleryDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<GalleryImage> DogImages { get; set; }
        public DbSet<ImageTag> ImgTags { get; set; }

    }
}
