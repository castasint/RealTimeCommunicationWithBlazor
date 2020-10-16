using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RealTimeCommunicationWithBlazor.Server.Data;
using RealTimeCommunicationWithBlazor.Server.Hubs;
using RealTimeCommunicationWithBlazor.Shared;

namespace RealTimeCommunicationWithBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IHubContext<BlogHub> _hubContext;

        public CommentsController(DatabaseContext context, IHubContext<BlogHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComment()
        {
            return await _context.Comment.OrderByDescending(x=>x.CommentId).ToListAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _context.Comment.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

     

        // POST: api/Comments
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            comment.CreatedAt = DateTime.UtcNow;
            _context.Comment.Add(comment);
            await _context.SaveChangesAsync();

            //Notify all the clients that a new comment has been added.
            await _hubContext.Clients.All.SendAsync("NewCommentAddition");
            

            return CreatedAtAction("GetComment", new { id = comment.CommentId }, comment);
        }

        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.CommentId == id);
        }
    }
}
