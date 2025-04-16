using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.PhotoPosts;
using ZeafloServer.Domain.Interfaces.IRepositories;

namespace ZeafloServer.Application.Queries.PhotoPosts.GetStorage
{
    public sealed class GetStorageQueryHandler : IRequestHandler<GetStorageQuery, List<StorageViewModel>>
    {
        private readonly IPhotoPostRepository _photoPostRepository;
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public GetStorageQueryHandler(
            IPhotoPostRepository photoPostRepository
        )
        {
            _photoPostRepository = photoPostRepository;
        }

        public async Task<List<StorageViewModel>> Handle(GetStorageQuery request, CancellationToken cancellationToken)
        {
            var query = _photoPostRepository.GetAllNoTracking().IgnoreQueryFilters();

            var yearStart = new DateTime(TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone).Year, 1, 1);
            var yearEnd = yearStart.AddYears(1);

            // 1. Get data from DB
            var postEntities = await query
                .Where(post => post.UserId == request.userId && post.SentAt >= yearStart && post.SentAt < yearEnd)
                .ToListAsync(cancellationToken);

            // 2. Group by {Month, Date}, choose 1 post random per day
            var random = new Random();
            var dailyRandomPosts = postEntities
                .GroupBy(p => new { Month = p.SentAt.Month, Date = p.SentAt.Date })
                .Select(g => g.OrderBy(_ => random.Next()).First())
                .ToList();

            // 3. Group by month
            var postsByMonth = dailyRandomPosts
                .GroupBy(p => p.SentAt.Month)
                .Select(g => StorageViewModel.FromStorage(
                    g.Key,
                    g.Select(p => new StoragePostViewModel
                    {
                        PhotoPostId = p.PhotoPostId,
                        Date = p.SentAt.Date,
                        ImageUrl = p.ImageUrl
                    }).ToList()
                ))
                .OrderBy(m => m.Month)
                .ToList();

            return postsByMonth;
        }
    }
}
