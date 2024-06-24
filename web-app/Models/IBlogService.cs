using System.Collections.Generic;
using System.Threading.Tasks;

namespace web_app.Models
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogArticle>> GetBlogArticlesAsync();
        Task<IEnumerable<BlogArticle>> GetPublishedBlogArticlesAsync();
        Task<IEnumerable<BlogArticle>> GetBlogArticlesByAuthorAsync(string author);
        Task<BlogArticle> GetBlogArticleByIdAsync(int id);
        Task<bool> CreateBlogArticleAsync(BlogArticle article);
        Task<bool> UpdateBlogArticleAsync(BlogArticle article);
        Task<bool> DeleteBlogArticleAsync(int id);
    }
}
