using FealtedByEsra.BAL.Abstract.Services.CommentService;
using FealtedByEsra.PL.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace FealtedByEsra.PL.ViewComponents.Comments
{
    public class CommentListByBlog : ViewComponent
    {
        private readonly ICommentService _service;
        private readonly IMemoryCache _cacheManager;
        private readonly ILogger<CommentListByBlog> _logger;

        public CommentListByBlog(ICommentService service, IMemoryCache cacheManager, ILogger<CommentListByBlog> logger)
        {
            _service = service;
            _cacheManager = cacheManager;
            _logger = logger;
        }

        private string Comment_CacheKey = "Comment_Key";

        public IViewComponentResult Invoke(int id)
        {
            try
            {
                var comments = _service.GetWhere(x => x.PostId == id, false);
                if (!_cacheManager.TryGetValue(Comment_CacheKey, out object list))
                {
                    if (comments != null && comments.Count() > 0)
                    {
                        _cacheManager.Set(Comment_CacheKey, comments, new MemoryCacheEntryOptions
                        {
                            AbsoluteExpiration = DateTime.Now.AddSeconds(3600),
                            SlidingExpiration = TimeSpan.FromSeconds(60),
                            Size = 1024,
                            Priority = CacheItemPriority.Normal
                        });
                    }
                }
                return View(comments);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Post yorum esnasından beklenmeyen hata {ex}.", ex);
                throw;
            }
        }
    }
}
