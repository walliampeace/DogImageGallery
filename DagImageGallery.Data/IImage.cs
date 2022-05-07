using DagImageGallery.Data.Models;
using System.Collections.Generic;

namespace DagImageGallery.Data
{
    public interface IImage
    {
        IEnumerable<GalleryImage> GetAll();
        IEnumerable<GalleryImage> GetWithTag(string tag);
        GalleryImage GetById(int id);
    }
}
