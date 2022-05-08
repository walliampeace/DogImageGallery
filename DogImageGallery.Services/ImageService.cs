using DagImageGallery.Data;
using DagImageGallery.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                .AsNoTracking()
                .Include(i => i.Tags)
                .OrderByDescending(i => i.Created)
                .ToList();
            //.Include(img => img.Tags);
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
        public CloudBlobContainer GetBlobContainer(string azureConnectionString, string containerName)
        {
            var storageAccount = CloudStorageAccount.Parse(azureConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference(containerName);
        }
        public List<ImageTag> ParseTags(string tags)
        {
            return tags.Split(", ")
              .Select(tag => new ImageTag
              {
                  Description = tag
              }).ToList();
        }
        // Only call down a range of images
        public IEnumerable<GalleryImage> Range(int skip, int take)
        {
            return _ctx.DogImages
                    .Include(i => i.Tags)
                    .OrderByDescending(i => i.Created)
                    .Skip(skip)
                    .Take(take)
                    .ToList();
        }
        public IEnumerable<GalleryImage> GetAllWithPaging(int pageNumber, int pageSize)
        {
            int skip = pageSize * (pageNumber - 1);
            int pageCount = _ctx.DogImages.Count();
            int capacity = skip + pageSize;
            bool hasNext = pageCount > capacity;

            return _ctx.DogImages
                    .Include(i => i.Tags)
                    .OrderByDescending(i => i.Created)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();
        }
        public void UpdateImage(GalleryImage changeImage)
        {
            _ctx.Entry(changeImage).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public void DeleteImage(int id)
        {
            var image = _ctx.DogImages
                .Include(i => i.Tags)
                .FirstOrDefault(i => i.Id == id);

            _ctx.DogImages.Remove(image);
            _ctx.SaveChanges();
        }
        public async Task SetImage(string title, string tags, Uri uri)
        {
            // create reference to SQL database
            var image = new GalleryImage
            {
                Title = title,
                Tags = ParseTags(tags), // handle tags that are null. Pass them as a form as a comma seperated from the list
                Url = uri.AbsoluteUri,
                Created = DateTime.Now
            };

            _ctx.Add(image);
            await _ctx.SaveChangesAsync();
        }
    }
}
