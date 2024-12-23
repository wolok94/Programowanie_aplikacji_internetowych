﻿using Programowanie_aplikacji_internetowych.domain.Dtos.Comments;
using Programowanie_aplikacji_internetowych.domain.Dtos.MetaDatas;
using Programowanie_aplikacji_internetowych.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.domain.Dtos.Posts;

public class GetPostByIdDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public string ImageUrl { get; set; }
    public MetaDataDto MetaData { get; set; }
    public IEnumerable<CommentDto> Comments { get; set; }
}
