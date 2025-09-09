using Microsoft.EntityFrameworkCore.Query;

namespace Deneme.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int ProjectId { get; set; } = 0;
        public Project? Project { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Content { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.Now;

    }
}
