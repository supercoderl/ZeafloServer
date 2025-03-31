using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.ViewModels.Likes
{
    public sealed class LikeViewModel
    {
        public Guid LikeId { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public DateTime CreatedAt { get; set; }

        public static LikeViewModel FromLike(Like like)
        {
            return new LikeViewModel
            {
                LikeId = like.LikeId,
                UserId = like.UserId,
                CreatedAt = like.CreatedAt,
                PostId = like.PostId,
            };
        }
    }
}
