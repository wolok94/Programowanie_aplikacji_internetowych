using Programowanie_aplikacji_internetowych.domain.Dtos.Posts;
using Programowanie_aplikacji_internetowych.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.domain.Interfaces.Services;

public interface IPostService
{
    Task CreatePost(CreatePostDto post);
    Task DeletePost(Guid id);
    Task<IEnumerable<GetPostsDto>> GetAllPosts();
    Task<Post> GetById(Guid id);
    Task UpdatePost(Guid id, UpdatePostDto postDto);
}
