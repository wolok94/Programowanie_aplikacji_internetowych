using Microsoft.AspNetCore.Mvc;
using Programowanie_aplikacji_internetowych.domain.Dtos.Comments;
using Programowanie_aplikacji_internetowych.domain.Interfaces.Services;


namespace Programowanie_aplikacji_internetowych.webapi.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [Route("createComment")]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Nieprawidłowy model komentarza");
            }
            await _commentService.CreateComment(commentDto);
            return Ok();
        }
    }
}
