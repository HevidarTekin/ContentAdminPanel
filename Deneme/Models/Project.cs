 using System.ComponentModel.DataAnnotations.Schema;

namespace Deneme.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string? Tags { get; set; }
        public bool PublishToBlog { get; set; }
        public int ViewCount { get; set; } = 0;
        public int LikeCount { get; set; } = 0;
        public string? GitHubLink { get; set; }


    }
}
