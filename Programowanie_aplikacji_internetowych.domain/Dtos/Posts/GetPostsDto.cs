﻿using Programowanie_aplikacji_internetowych.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.domain.Dtos.Posts;

public class GetPostsDto
{
    public string Title { get; set; }
    public string Text { get; set; }
    public MetaData MetaData { get; set; }
    public int NumberOfComments { get; set; }
}