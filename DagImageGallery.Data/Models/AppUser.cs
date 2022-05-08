using Microsoft.AspNetCore.Identity;
using System;

namespace DagImageGallery.Data.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public string DisplayName { get; set; }
    }
}
