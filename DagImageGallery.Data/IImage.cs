using DagImageGallery.Data.Models;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DagImageGallery.Data
{
    public interface IImage
    {
        IEnumerable<GalleryImage> GetAll();
        IEnumerable<GalleryImage> GetWithTag(string tag);
        GalleryImage GetById(int id);
        CloudBlobContainer GetBlobContainer(string connectionString, string container);
        Task SetImage(string title, string tags, Uri uri);
        List<ImageTag> ParseTags(string tags);
        IEnumerable<GalleryImage> Range(int skip, int take);

        void UpdateImage(GalleryImage changeImage);

        void DeleteImage(int id);
        IEnumerable<GalleryImage> GetAllWithPaging(int pageNumber, int pageSize);
    }
}
