using System;
using Aprenda.Backend.Models;
namespace Aprenda.Backend.Services.Post;

public interface IPostService
{

    Task<IEnumerable<Dtos.Post.PostDto>> GetAllPostsByClassroomIdAsync(long classroomId);
    Task<Dtos.Post.PostDto> GetPostByIdAsync(long id);
    Task<Dtos.Post.PostDto> CreatePostAsync(long userId, long classroomId, Dtos.Post.CreatePostDto Post);
    Task UpdatePostAsync(long userId, long idPost, Dtos.Post.CreatePostDto Post);
    Task DeletePostAsync(long userId, long idPost);

}
