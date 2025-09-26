using System;
using Aprenda.Backend.Dtos.Post;
using Aprenda.Backend.Dtos.User;
using Aprenda.Backend.Mappers.Post;
using Aprenda.Backend.Mappers.User;
using Aprenda.Backend.Models;
using Aprenda.Backend.Repositories.Archive;
using Aprenda.Backend.Repositories.Classroom;
using Aprenda.Backend.Repositories.Post;
using Aprenda.Backend.Repositories.User;
using Aprenda.Backend.Services.User;
namespace Aprenda.Backend.Services.Post;

public class PostService : IPostService
{
    private readonly IPostRepository _PostRepository;
    private readonly IUserRepository _UserRepository;

    private readonly IClassroomRepository _ClassroomRepository;

    private readonly IArchiveRepository _ArchiveRepository;

    public PostService(IPostRepository PostRepository, IUserRepository userRepository, IClassroomRepository classroomRepository, IArchiveRepository archiveRepository)
    {
        _PostRepository = PostRepository;
        _UserRepository = userRepository;
        _ClassroomRepository = classroomRepository;
        _ArchiveRepository = archiveRepository;
    }

    public async Task<PostDto> CreatePostAsync(long userId, long classroomId, CreatePostDto Post)
    {
        var professor = await _UserRepository.GetByIdAsync(userId);
        if (professor == null || professor.Profile != EProfile.Professor)
        {
            throw new KeyNotFoundException("User not found or is not a professor");
        }

        var classroom = await _ClassroomRepository.GetByIdAsync(classroomId);
        if (classroom == null)
        {
            throw new KeyNotFoundException("Classroom not found");
        }

        if (!classroom.Users.Any(u => u.Id == professor.Id))
        {
            throw new InvalidOperationException("This professor is not part of the classroom");
        }


        var PostEntity = PostMapper.ToDomain(Post);

        if (Post.AttachmentIds != null && Post.AttachmentIds.Any())
        {
            var archives = await _ArchiveRepository.GetByIdsAsync(Post.AttachmentIds);

            if (archives.Count() != Post.AttachmentIds.Count())
            {
                throw new KeyNotFoundException("Um ou mais arquivos anexados não foram encontrados.");
            }

            foreach (var archive in archives)
            {
                PostEntity.Archives.Add(archive);
            }

        }
        PostEntity.UserId = professor.Id;
        PostEntity.ClassroomId = classroom.Id;
        PostEntity.CreatedAt = DateTime.UtcNow;
        await _PostRepository.AddAsync(PostEntity);
        return PostEntity.ToDto();
    }

    public async Task DeletePostAsync(long id)
    {
        await _PostRepository.DeleteAsync(id);
    }

    public async Task DeletePostAsync(long userId, long idPost)
    {
        var post = await _PostRepository.GetByIdAsync(idPost);
        if (post == null)
        {
            throw new KeyNotFoundException("Post not found");
        }

        if (post.UserId != userId)
        {
            throw new UnauthorizedAccessException("You are not authorized to delete this post");
        }

        await _PostRepository.DeleteAsync(idPost);
    }

    public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
    {
        var Posts = await _PostRepository.GetAllAsync();
        return Posts.Select(c => c.ToDto());
    }

    public async Task<IEnumerable<PostDto>> GetAllPostsByClassroomIdAsync(long classroomId)
    {
        var Posts = await _PostRepository.GetPostsByClassroomIdAsync(classroomId);
        return Posts.Select(c => c.ToDto());
    }

    public async Task<PostDto> GetPostByIdAsync(long id)
    {
        return await _PostRepository.GetByIdAsync(id) is Models.Post Post ? Post.ToDto() : throw new KeyNotFoundException("Post not found");
    }

    public async Task UpdatePostAsync(long userId, long idPost, CreatePostDto Post)
    {
        var PostEntity = await _PostRepository.GetByIdAsync(idPost) ?? throw new KeyNotFoundException("Post not found");
        if (PostEntity.UserId != userId)
        {
            throw new UnauthorizedAccessException("You are not authorized to update this post");
        }
        PostEntity.Title = Post.Title;
        PostEntity.Content = Post.Content;
        PostEntity.IsFixed = Post.IsFixed;
        PostEntity.CreatedAt = DateTime.UtcNow;

        await _PostRepository.UpdateAsync(PostEntity);
    }



}
