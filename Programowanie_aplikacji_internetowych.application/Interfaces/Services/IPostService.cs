using Microsoft.AspNetCore.Http;
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
    Task<IEnumerable<Post>> CreatePostFromCsv(IFormFile file);
    Task DeletePost(Guid id);
    Task<IEnumerable<GetPostsDto>> GetAllPosts();
    Task<GetPostByIdDto> GetById(Guid id);
    Task UpdatePost(Guid id, UpdatePostDto postDto);
}
