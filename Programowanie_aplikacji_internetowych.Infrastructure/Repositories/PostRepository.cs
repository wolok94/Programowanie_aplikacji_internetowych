using Microsoft.EntityFrameworkCore;
using Programowanie_aplikacji_internetowych.domain.Entities;
using Programowanie_aplikacji_internetowych.domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.Infrastructure.Repositories;

public class PostRepository : GenericRepository<Post>, IPostRepository
{
    private readonly AppDbContext _dbContext;

    public PostRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override async Task<IEnumerable<Post>> GetAll()
    {
        return await _dbContext.Posts
                        .Include(x => x.MetaData)
                        .Include(x => x.Comments)
                        .ToListAsync();
    }

    public override async Task<Post> GetById(Guid id) => await _dbContext.Posts
            .Include(x => x.MetaData)
            .ThenInclude(x => x.User)
            .Include(x => x.Comments)
            .FirstOrDefaultAsync(x => x.Id == id);
}
