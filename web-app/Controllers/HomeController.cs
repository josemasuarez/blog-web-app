using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using web_app.Models;
using Microsoft.AspNetCore.Authorization;

namespace web_app.Controllers;

[Authorize(Policy = "ViewerOrAdmin")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBlogService _blogService;

    public HomeController(ILogger<HomeController> logger, IBlogService blogService)
    {
        _logger = logger;
        _blogService = blogService;
    }

        public async Task<IActionResult> Index()
        {
            var publishedArticles = await _blogService.GetPublishedBlogArticlesAsync();
            return View(publishedArticles);
        }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
