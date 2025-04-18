﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Sorting;
using ZeafloServer.Application.ViewModels;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Application.ViewModels.Posts;

namespace ZeafloServer.Application.Interfaces
{
    public interface IPostService
    {
        public Task<PageResult<PostViewModel>> GetAllPostsAsync(
            PageQuery query,
            ActionStatus status,
            string scope = "others",
            string searchTerm = "",
            Guid? userId = null,
            SortQuery? sortQuery = null
        );
        public Task<Guid> CreatePostAsync(CreatePostRequest request);
        public Task<Guid> ReactPostAsync(ReactPostRequest request);
        public Task<Guid> SavePostAsync(SavePostRequest request);
    }
}
