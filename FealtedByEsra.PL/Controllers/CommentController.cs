using FealtedByEsra.BAL.Abstract.Services.CommentService;
using FealtedByEsra.BAL.Abstract.Services.PostService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FealtedByEsra.PL.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;
        private readonly ILogger<CommentController> _logger;
        public CommentController(ICommentService commentService, IPostService postService, ILogger<CommentController> logger)
        {
            _commentService = commentService;
            _postService = postService;
            _logger = logger;
        }

        public IActionResult GetComment()
        {
            try
            {
                if (User!.Identity!.IsAuthenticated)
                {
                    var comments = _commentService.GetAll(false);                    
                    return View(comments);
                }
                return NotFound();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> RemoveComment(int id)
        {
            try
            {
                var post = await _commentService.GetByIdAsync(id);
                if (post == null)
                    return NotFound();
                await _commentService.RemoveAsync(id);
                TempData["RemoveComment"] = "Yorum başarıyla silindi!";
                return RedirectToAction("GetComment", "Comment", new { page= 1});
            }
            catch (Exception ex)
            {
                _logger.LogError($"Post silme sırasında beklenmeyen hata {ex}.", ex);
                return BadRequest();
            }
        }
    }
}
