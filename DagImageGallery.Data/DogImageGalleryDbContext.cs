using DagImageGallery.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DagImageGallery.Data
{
    public class DogImageGalleryDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public DogImageGalleryDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<GalleryImage> DogImages { get; set; }
        public DbSet<ImageTag> ImgTags { get; set; }

    }
}
