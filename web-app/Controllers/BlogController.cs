using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using web_app.Models;

namespace web_app.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [Authorize(Policy = "AdminOrEditor")]
        public async Task<IActionResult> Index()
        {
            var author = HttpContext.Session.GetString("Username");
            var articles = await _blogService.GetBlogArticlesByAuthorAsync(author);
            return View(articles);
        }

        [Authorize(Policy = "AdminOrEditor")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOrEditor")]
        public async Task<IActionResult> Create(BlogArticle article)
        {
            article.Author = HttpContext.Session.GetString("Username");

            await _blogService.CreateBlogArticleAsync(article);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "AdminOrEditor")]
        public async Task<IActionResult> Edit(int id)
        {
            var article = await _blogService.GetBlogArticleByIdAsync(id);
            if (article == null || article.IsPublished)
            {
                return NotFound();
            }
            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOrEditor")]
        public async Task<IActionResult> Edit(BlogArticle article)
        {
            await _blogService.UpdateBlogArticleAsync(article);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "AdminOrEditor")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var article = await _blogService.GetBlogArticleByIdAsync(id);
            if (article == null || article.IsPublished)
            {
                return NotFound();
            }
            return View(article);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOrEditor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _blogService.DeleteBlogArticleAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "ViewerOrAdminOrEditor")]
        public async Task<IActionResult> Details(int id)
        {
            var article = await _blogService.GetBlogArticleByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        [Authorize(Policy = "AdminOrEditor")]
        public async Task<IActionResult> Publish(int id)
        {
            var article = await _blogService.GetBlogArticleByIdAsync(id);
            if (article == null || article.IsPublished)
            {
                return NotFound();
            }
            return View(article);
        }

        [HttpPost, ActionName("PublishConfirmed")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOrEditor")]
        public async Task<IActionResult> PublishConfirmed(int id)
        {
            var article = await _blogService.GetBlogArticleByIdAsync(id);
            if (article == null || article.IsPublished)
            {
                return NotFound();
            }

            article.IsPublished = true;
            var success = await _blogService.UpdateBlogArticleAsync(article);
            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
