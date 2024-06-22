using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index()
        {
            var articles = await _blogService.GetBlogArticlesAsync();
            return View(articles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogArticle article)
        {
            if (ModelState.IsValid)
            {
                await _blogService.CreateBlogArticleAsync(article);
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

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
        public async Task<IActionResult> Edit(BlogArticle article)
        {
            if (ModelState.IsValid)
            {
                await _blogService.UpdateBlogArticleAsync(article);
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var article = await _blogService.GetBlogArticleByIdAsync(id);
            if (article == null || article.IsPublished)
            {
                return NotFound();
            }
            return View(article);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _blogService.DeleteBlogArticleAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var article = await _blogService.GetBlogArticleByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        [HttpPost, ActionName("Publish")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Publish(int id)
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
