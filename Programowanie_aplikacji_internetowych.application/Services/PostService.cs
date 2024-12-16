using Programowanie_aplikacji_internetowych.domain.Dtos.Comments;
using Programowanie_aplikacji_internetowych.domain.Dtos.MetaDatas;
using Programowanie_aplikacji_internetowych.domain.Dtos.Posts;
using Programowanie_aplikacji_internetowych.domain.Entities;
using Programowanie_aplikacji_internetowych.domain.Interfaces.Repository;
using Programowanie_aplikacji_internetowych.domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.application.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IUserContextService _userContext;

    public PostService(IPostRepository postRepository, IUserContextService userContext)
    {
        _postRepository = postRepository;
        _userContext = userContext;
    }

    public async Task CreatePost(CreatePostDto post)
    {
        var newPost = new Post
        {
            Text = post.Text,
            Title = post.Title,
            MetaData = new MetaData
            {
                CreatedDate = DateTime.Now,
                UserId = _userContext.UserId.Value
            },
            ImageUrl = post.ImageUrl
        };
        await _postRepository.AddAsync(newPost);
    }

    public async Task<IEnumerable<GetPostsDto>> GetAllPosts()
    {
        var posts = await _postRepository.GetAll();
        var postsDto = posts.Select(x => new GetPostsDto
        {
            Id = x.Id,
            MetaData = new MetaDataDto
            {
                CreatedDate = x.MetaData.CreatedDate,
                ModifiedById = x.MetaData.ModifiedById,
                ModifiedDate = x.MetaData.ModifiedDate,
                UserId = x.MetaData.UserId
            },
            NumberOfComments = x.Comments.Count(),
            Text = x.Text,
            Title = x.Title,
            ImageUrl = x.ImageUrl
        });
        return postsDto;
    }

    public async Task<GetPostByIdDto> GetById(Guid id)
    {
        var post = await _postRepository.GetById(id);

        var postDto = new GetPostByIdDto
        {
            Comments = post.Comments.Select(x => new CommentDto
            {
                Id = x.Id,
                Text = x.Text,
                MetaData = new MetaDataDto
                {
                    CreatedDate = x.MetaData.CreatedDate,
                    ModifiedById = x.MetaData.ModifiedById,
                    ModifiedDate = x.MetaData.ModifiedDate,
                    UserId = x.MetaData.UserId
                }
            }),
            MetaData = new MetaDataDto
            {
                CreatedDate = post.MetaData.CreatedDate,
                ModifiedById = post.MetaData.ModifiedById,
                ModifiedDate = post.MetaData.ModifiedDate,
                UserId = post.MetaData.UserId
            },
            Id = post.Id,
            Text = post.Text,
            Title = post.Title,
            ImageUrl = post.ImageUrl
        };

        return postDto;
    }

    public async Task DeletePost(Guid id)
    {
        var post = await _postRepository.GetById(id);
        await _postRepository.DeleteAsync(post);
    }

    public async Task UpdatePost(Guid id, UpdatePostDto postDto)
    {
        var post = await _postRepository.GetById(id);
        post.Text = postDto.Text;
        post.Title = postDto.Title;
        post.MetaData.ModifiedDate = DateTime.Now;
        post.MetaData.ModifiedById = _userContext.UserId;
        post.ImageUrl = postDto.ImageUrl;
        await _postRepository.UpdateAsync(post);
    }
}
