using Microsoft.EntityFrameworkCore;

namespace web_app.Models
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
        }

        public DbSet<BlogArticle> BlogArticles { get; set; }
    }
}