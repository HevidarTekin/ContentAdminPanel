using Deneme.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Deneme.Context
{

    public class BlogDbContext : IdentityDbContext<ApplicationUser>
    {
      
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Project> Projects { get; set; }
      
    }
}
