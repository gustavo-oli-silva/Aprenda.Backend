using System;
using Aprenda.Backend.Dtos.Post;
using Aprenda.Backend.Mappers.Archive;
using Aprenda.Backend.Mappers.User;

namespace Aprenda.Backend.Mappers.Post;

public static class PostMapper
{
    public static PostDto ToDto(this Models.Post Post,  IHttpContextAccessor httpContextAccessor) =>
        new PostDto(
            Post.Id,
            Post.Title,
            Post.Content,
            Post.IsFixed,
            UserMapper.ToDto(Post.User),
            Post.ClassroomId,
            Post.Archives.Select(a => a.ToDto(httpContextAccessor)),
            Post.CreatedAt
        );


    public static Models.Post ToDomain(this CreatePostDto createDto) =>
        new Models.Post
        {
            Title = createDto.Title,
            Content = createDto.Content,
            IsFixed = createDto.IsFixed,
        };
}
