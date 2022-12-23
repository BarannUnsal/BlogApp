using FealtedByEsra.BAL.Abstract.Services.PostService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using X.PagedList;

namespace FealtedByEsra.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache _cacheManager;

        public HomeController(IPostService postService, ILogger<HomeController> logger, IMemoryCache cacheManager)
        {
            _postService = postService;
            _logger = logger;
            _cacheManager = cacheManager;
        }

        private string Post_CacheKey = "Post_CacheKey";

        public IActionResult Index(int? category, int page = 1)
        {
            try
            {
                if(category != null)
                {
                    ViewBag.cat = category;
                }

                var posts = _postService.GetPostByCategory(category, false).ToPagedList(page, 4);
                var categoryName = _postService.GetPostByCategory(category);
                if (!_cacheManager.TryGetValue(Post_CacheKey, out object list))
                {
                    if (posts != null && posts.Count() > 0)
                    {
                        _cacheManager.Set(Post_CacheKey, posts, new MemoryCacheEntryOptions
                        {
                            AbsoluteExpiration = DateTime.Now.AddSeconds(3600),
                            SlidingExpiration = TimeSpan.FromSeconds(60),
                            Size = 1024,
                            Priority = CacheItemPriority.Normal
                        });
                    }
                }
                return View(posts);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hata ile karşılaşıldı {ex}.", ex);
                throw;
            }
        }
    }
}