using Programowanie_aplikacji_internetowych.domain.Dtos.Comments;
using Programowanie_aplikacji_internetowych.domain.Entities;
using Programowanie_aplikacji_internetowych.domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.domain.Interfaces.Services;

public interface ICommentService
{
    Task CreateComment(CreateCommentDto commentDto);
}
