using MediatR;
using ZeafloServer.Application.ViewModels.PhotoPosts;

namespace ZeafloServer.Application.Queries.PhotoPosts.GetStorage
{
    public sealed record GetStorageQuery(Guid userId) : IRequest<List<StorageViewModel>>;
}
