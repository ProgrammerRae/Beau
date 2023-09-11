using Beau.Data;
using Beau.Models;
using Google;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public class PostRepository
{
    private readonly DataBContext _context;

    public PostRepository(DataBContext context)
    {
        _context = context;
    }

    public List<Post> GetPostsByUserId(Guid userId)
    {

        return _context.Posts
            .Where(p => p.UserId == userId)
            .ToList();
    }
}
