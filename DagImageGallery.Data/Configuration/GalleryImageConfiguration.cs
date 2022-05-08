using DagImageGallery.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DagImageGallery.Data.Configuration
{
    public class GalleryImageConfiguration
    {   
        public void Configure(EntityTypeBuilder<GalleryImage> builder)
        {
            builder.ToTable("GalleryImages");
        }
    }
}
