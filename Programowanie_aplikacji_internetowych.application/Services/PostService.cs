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
            }
        };
        await _postRepository.AddAsync(newPost);
    }

    public async Task<IEnumerable<Post>> GetAllPosts()
    {
        return await _postRepository.GetAll();
    }
}
