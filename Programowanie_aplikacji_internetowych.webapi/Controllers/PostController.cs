using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Programowanie_aplikacji_internetowych.domain.Dtos.Posts;
using Programowanie_aplikacji_internetowych.domain.Entities;
using Programowanie_aplikacji_internetowych.domain.Interfaces.Services;

namespace Programowanie_aplikacji_internetowych.webapi.Controllers;
[ApiController]
[Route("api/Post")]
[Authorize()]
public class PostController : Controller
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpPost]
    [Route("createPost")]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto post)
    {
        if (!ModelState.IsValid)
        {
            throw new ArgumentException("Nieprawidłowy model postu");
        }
        await _postService.CreatePost(post);
        return Ok();
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllPosts()
    {
        var posts = await _postService.GetAllPosts();
        return Ok(posts);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var post = await _postService.GetById(id);
        return Ok(post);
    }
}
