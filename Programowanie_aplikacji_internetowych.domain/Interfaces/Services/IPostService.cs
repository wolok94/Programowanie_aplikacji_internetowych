﻿using Programowanie_aplikacji_internetowych.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.domain.Interfaces.Services;

public interface IPostService
{
    Task CreatePost(Post post);
}
