using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace RealTimeCommunicationWithBlazor.Shared
{
   public class Blog
    {
        public int BlogId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [MinLength(10,ErrorMessage ="Blog Should have atleast 10 characters")]
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }
}
