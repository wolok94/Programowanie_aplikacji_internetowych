﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.domain.Dtos.Comments;

public class CreateCommentDto
{
    public string Text { get; set; }
    public Guid PostId { get; set; }
}