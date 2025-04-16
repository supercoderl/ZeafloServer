using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Application.ViewModels.PhotoPosts
{
    public sealed class StorageViewModel
    {
        public int Month { get; set; }
        public List<StoragePostViewModel> PhotoPosts { get; set; } = new List<StoragePostViewModel>();

        public static StorageViewModel FromStorage(
            int month,
            List<StoragePostViewModel> photoPosts
        )
        {
            return new StorageViewModel
            {
                Month = month,
                PhotoPosts = photoPosts
            };
        }
    }

    public class StoragePostViewModel
    {
        public Guid PhotoPostId { get; set; }
        public DateTime Date { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
