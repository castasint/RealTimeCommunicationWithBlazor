using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using RealTimeCommunicationWithBlazor.Server.Data;
using RealTimeCommunicationWithBlazor.Server.Hubs;
using RealTimeCommunicationWithBlazor.Shared;

namespace RealTimeCommunicationWithBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IHubContext<BlogHub> _hubContext;

        public BlogsController(DatabaseContext context, IHubContext<BlogHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlog()
        {
        var comments = await _context.Blog.Include(x=>x.Comments).OrderByDescending(x=>x.BlogId).ToListAsync();
            return comments;
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlog(int id)
        {
            var blog = await _context.Blog.Where(x => x.BlogId == id).Include(y => y.Comments).FirstOrDefaultAsync();

            if (blog == null)
            {
                return NotFound();
            }

            return blog;
        }

 

        // POST: api/Blogs
    
        [HttpPost]
        public async Task<ActionResult<Blog>> PostBlog(Blog blog)
        {
            blog.CreatedAt =  DateTime.UtcNow;
            _context.Blog.Add(blog);
            await _context.SaveChangesAsync();

            //Send a message to all connected clients that a new blog has been added.
            await _hubContext.Clients.All.SendAsync("NewBlogAddition");

            return CreatedAtAction("GetBlog", new { id = blog.BlogId }, blog);
        }



        private bool BlogExists(int id)
        {
            return _context.Blog.Any(e => e.BlogId == id);
        }
    }
}
