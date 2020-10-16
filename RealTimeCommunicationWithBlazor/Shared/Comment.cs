using System;
using System.Collections.Generic;
using System.Text;

namespace RealTimeCommunicationWithBlazor.Shared
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual int BlogId { get; set; }
    }
}
