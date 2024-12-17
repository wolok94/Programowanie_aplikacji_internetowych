using Programowanie_aplikacji_internetowych.domain.Dtos.Comments;
using Programowanie_aplikacji_internetowych.domain.Entities;
using Programowanie_aplikacji_internetowych.domain.Interfaces.Repository;
using Programowanie_aplikacji_internetowych.domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserContextService _userContextService;

    public CommentService(ICommentRepository commentRepository, IUserContextService userContextService)
    {
        _commentRepository = commentRepository;
        _userContextService = userContextService;
    }

    public async Task CreateComment(CreateCommentDto commentDto)
    {
        var comment = new Comment
        {
            PostId = commentDto.PostId,
            Text = commentDto.Text,
            MetaData = new MetaData
            {
                CreatedDate = DateTime.Now,
                UserId = _userContextService.UserId.Value
            }
        };

        await _commentRepository.AddAsync(comment);
    }
}
