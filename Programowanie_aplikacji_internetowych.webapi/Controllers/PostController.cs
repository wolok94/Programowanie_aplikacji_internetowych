using Microsoft.AspNetCore.Mvc;
using Programowanie_aplikacji_internetowych.domain.Entities;
using Programowanie_aplikacji_internetowych.domain.Interfaces.Services;

namespace Programowanie_aplikacji_internetowych.webapi.Controllers;
[ApiController]
[Route("api/Post")]
public class PostController : Controller
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpPost]
    [Route("createPost")]
    public async Task<IActionResult> CreatePost([FromBody] Post post)
    {
        if (!ModelState.IsValid)
        {
            throw new ArgumentException("Nieprawidłowy model postu");
        }
        await _postService.CreatePost(post);
        return Ok();
    }
}
