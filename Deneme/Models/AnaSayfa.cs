namespace Deneme.Models
{
    public class AnaSayfa
    {

           public int Id { get; set; } = 0;
           public int? ProjectId { get; set; } = 0;
            public string? Name { get; set; }
            public string? Description { get; set; }
            public string? ImageUrl { get; set; }
            public DateTime PublishDate { get; set; }
            public string? Tags { get; set; }
            public int LikeCount { get; set; }
            public int ViewCount { get; set; }
            public int CommentCount { get; set; }
            public int Status { get; set; }

        }
    }



