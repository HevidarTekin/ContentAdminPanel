using Deneme.Models;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Deneme.ViewModels
{
    public class ViewDetailsModel
    {
        public Project? Project { get; set; }
        public List<Comment>? Comments { get; set; }
        public Comment? NewComment { get; set; } = new Comment();

        public int CommentCount => Comments?.Count ?? 0;
        

    }
}
