using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace web_app.Models
{
    public class BlogService : IBlogService
    {
        private readonly BlogContext _context;

        public BlogService(BlogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BlogArticle>> GetBlogArticlesAsync()
        {
            return await _context.BlogArticles.ToListAsync();
        }

        public async Task<IEnumerable<BlogArticle>> GetBlogArticlesByAuthorAsync(string author)
        {
            return await _context.BlogArticles
                                 .Where(article => article.Author == author)
                                 .ToListAsync();
        }

        public async Task<BlogArticle> GetBlogArticleByIdAsync(int id)
        {
            return await _context.BlogArticles.FindAsync(id);
        }

        public async Task<IEnumerable<BlogArticle>> GetPublishedBlogArticlesAsync()
        {
            return await _context.BlogArticles.Where(a => a.IsPublished).ToListAsync();
        }

        public async Task<bool> CreateBlogArticleAsync(BlogArticle article)
        {
            article.CreatedDate = DateTime.Now;
            article.UpdatedDate = DateTime.Now;
            _context.BlogArticles.Add(article);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateBlogArticleAsync(BlogArticle article)
        {
            var existingArticle = await _context.BlogArticles.FindAsync(article.Id);
            if (existingArticle == null)
            {
                return false;
            }
            existingArticle.Title = article.Title;
            existingArticle.Description = article.Description;
            existingArticle.UpdatedDate = DateTime.Now;
            existingArticle.IsPublished = article.IsPublished;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBlogArticleAsync(int id)
        {
            var article = await _context.BlogArticles.FindAsync(id);
            if (article == null || article.IsPublished) return false;

            _context.BlogArticles.Remove(article);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}