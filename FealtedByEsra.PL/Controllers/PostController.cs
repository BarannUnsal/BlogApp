using FealtedByEsra.BAL.Abstract.Services;
using FealtedByEsra.BAL.Abstract.Services.CategoryService;
using FealtedByEsra.BAL.Abstract.Services.CommentService;
using FealtedByEsra.BAL.Abstract.Services.GoogleService;
using FealtedByEsra.BAL.Abstract.Services.MailService;
using FealtedByEsra.BAL.Abstract.Services.PostService;
using FealtedByEsra.BAL.Abstract.Services.ReplyService;
using FealtedByEsra.BAL.Extensions;
using FealtedByEsra.BAL.Filters;
using FealtedByEsra.BAL.Helpers.Image;
using FealtedByEsra.BAL.Helpers.UrlHelpers;
using FealtedByEsra.BAL.Validations.PostValidate;
using FealtedByEsra.Entity.Entities;
using FealtedByEsra.PL.Models.ckEditor;
using FealtedByEsra.PL.Models.Comment;
using FealtedByEsra.PL.Models.Post;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using X.PagedList;

namespace FealtedByEsra.PL.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMemoryCache _cacheManager;
        private readonly FileBaseUploader _fileBaseUploader;
        private readonly ImageHelper _imageHelper;
        private readonly ILogger<PostController> _logger;
        private readonly ICommentService _commentService;
        private readonly IReplyCommentService _replyCommentService;
        private readonly IGoogleService _googleService;
        private readonly ICategoryService _categoryService;
        private readonly IEmailService _emailService;
        private readonly INewsletterService _newsletterService;

        public PostController(IPostService postService, IMemoryCache cacheManager, FileBaseUploader fileBaseUploader, ImageHelper imageHelper, ILogger<PostController> logger, ICommentService commentService, IReplyCommentService replyCommentService, IGoogleService googleService, ICategoryService categoryService, IEmailService emailService, INewsletterService newsletterService)
        {
            _postService = postService;
            _cacheManager = cacheManager;
            _fileBaseUploader = fileBaseUploader;
            _imageHelper = imageHelper;
            _logger = logger;
            _commentService = commentService;
            _replyCommentService = replyCommentService;
            _googleService = googleService;
            _categoryService = categoryService;
            _emailService = emailService;
            _newsletterService = newsletterService;
        }

        private string Post_CacheKey = "Post_CacheKey";

        [Authorize]
        public IActionResult GetPost()
        {
            try
            {
                if (User!.Identity!.IsAuthenticated)
                {
                    var posts = _postService.GetAll(false);

                    if (!_cacheManager.TryGetValue(Post_CacheKey, out object list))
                    {
                        if (posts != null)
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
                    _logger.LogInformation("Yazılara erişildi.");
                    return View(posts);
                }
                else
                {

                    _logger.LogWarning("Kimliği doğurlanmayan kullanıcı post sayfasına erişmeye çalıştı.");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hata ile karşılaşıldı {ex}.", ex);
                throw;
            }
        }

        public async Task<IActionResult> SearchPost(string? text, int page = 1)
        {
            try
            {
                if (text != null)
                {
                    ViewBag.txt = text;
                    if (text == null)
                        return View();
                    var posts = await _postService.SearchPost(text);
                    var pagedPosts = await posts.ToPagedListAsync(page, 5);
                    return View(pagedPosts);
                }
                else
                    return View(text);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Arama yapılırken beklenmeyen hata {ex}.", ex);
                throw;
            }
        }

        [Route("post/details/{id}/{title}")]
        public async Task<IActionResult> Details(int id, string? title)
        {
            try
            {
                FriendlyUrllHelper.GenerateTitle(title);

                var post = await _postService.GetPostIdAsync(id);

                var prevID = _postService.GetAll()
                         .OrderBy(i => i.Id)
                         .Where(i => i.Id < id)
                         .FirstOrDefault();
                if (prevID != null)
                {
                    ViewBag.prevID = prevID.Id;
                    ViewBag.prevtitle = prevID.Title;
                    ViewBag.prevthumbnail = prevID.Thumbnail;
                }

                var nextID = _postService.GetAll()
                        .OrderBy(i => i.Id)
                        .Where(i => i.Id > id)
                        .FirstOrDefault();
                if (nextID != null)
                {
                    ViewBag.NextID = nextID.Id;
                    ViewBag.nexttitle = nextID.Title;
                    ViewBag.nextthumbnail = nextID.Thumbnail;
                }

                if (!_cacheManager.TryGetValue(Post_CacheKey, out object _))
                {
                    if (post != null)
                    {
                        _cacheManager.Set(Post_CacheKey, post, new MemoryCacheEntryOptions
                        {
                            AbsoluteExpiration = DateTime.Now.AddSeconds(3600),
                            SlidingExpiration = TimeSpan.FromSeconds(60),
                            Size = 1024,
                            Priority = CacheItemPriority.Normal
                        });
                    }
                }
                _logger.LogInformation($"Post detay sayfasına erişildi {post!.Title.ToUpper()}.");
                return View(post);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Post detay sayfasında hata ile karşılaşıldı {ex}.", ex);
                throw;
            }
        }

        [Authorize]
        [Route("post/addpost")]
        public IActionResult AddPost()
        {
            try
            {
                if (User!.Identity!.IsAuthenticated)
                {
                    ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "Id", "CategoryName");
                    _logger.LogInformation("Post ekleme sayfasına erişildi.");
                    return View();
                }
                else
                {
                    _logger.LogWarning("Kimliği doğurlanmayan kullanıcı kategori sayfasına erişmeye çalıştı.");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Post ekleme sayfasında beklenmeyen hata {ex}.", ex);
                throw;
            }
        }

        public async Task<JsonResult> OnPostUploadImage([FromForm] IFormFile upload)
        {
            if (upload.Length <= 0)
                return new JsonResult(NotFound());

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();

            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/CKEditorImages",
                fileName);

            using (var image = await Image.LoadAsync(upload.OpenReadStream()))
            {
                string newSize = _imageHelper.ImageResize(image, 1296, 600);
                string[] sizeArray = newSize.Split(",");
                image.Mutate(x => x.Resize(Convert.ToInt32(sizeArray[1]), Convert.ToInt32(sizeArray[0])));
                await image.SaveAsJpegAsync(filePath);
            }

            var url = $"{"/CKEditorImages/"}{fileName}";

            var success = new uploadsuccess
            {
                Uploaded = 1,
                FileName = fileName,
                Url = url
            };

            return new JsonResult(success);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [Route("post/addpost")]
        public async Task<IActionResult> AddPost(CreatePostViewModel model)
        {
            try
            {
                if (User!.Identity!.IsAuthenticated)
                {
                    Post post = new()
                    {
                        Title = model.Title,
                        Description = model.Description,
                        CategoryId = model.CategoryId,
                        CreatedDate = model.CreatedDate,
                    };

                    ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(false), "Id", "CategoryName", post.CategoryId);

                    if (model.Thumbnail != null)
                    {
                        var extension = "Jpeg";
                        var newImageName = $"{Guid.NewGuid()}.{extension}";
                        string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/PostThumbnail", newImageName));
                        using (var image = await Image.LoadAsync(model.Thumbnail.OpenReadStream()))
                        {
                            string newSize = _imageHelper.ImageResize(image, 1200, 700);
                            string[] sizeArray = newSize.Split(",");
                            image.Mutate(x => x.Resize(Convert.ToInt32(sizeArray[1]), Convert.ToInt32(sizeArray[0])));
                            await image.SaveAsJpegAsync(path);
                        }
                        post.Thumbnail = newImageName;
                    }

                    PostValidation validations = new();

                    ValidationResult result = await validations.ValidateAsync(post);

                    var news = _newsletterService.GetAll(false)?.ToList();

                    int newsId;

                    foreach (var _new in news)
                    {
                        newsId = _new.Id;
                    }

                    if (result.IsValid)
                    {
                        _logger.LogInformation($"Post başarıyla eklendi {post.Title.ToUpper()}.");
                        await _postService.AddAsync(post);
                        if(news.Count() > 0)
                            foreach (var _new in news)
                            {
                                newsId = _new.Id;
                                _emailService.NewPostEmail(news, post.Title, post.Thumbnail, post.Description, newsId, post.Id);
                            }
                        TempData["AddPost"] = "Başarıyla Eklendi!";
                        return RedirectToAction(nameof(GetPost));
                    }
                    else
                    {
                        result.AddToModelState(this.ModelState);
                        return View(model);
                    }
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Post ekleme sırasında beklenmeyen hata {ex}.", ex);
                throw;
            }
        }


        [Route("post/comment")]
        public async Task<IActionResult> Comment(CommentViewModel model)
        {
            try
            {

                var post = await _postService.GetPostIdAsync(model.PostId);

                if (model.MainId == 0)
                {
                    if (ModelState.IsValid)
                    {
                        var captchaResult = await _googleService.VertifyToken(model!.Token!);
                        if (!captchaResult)
                            throw new BadHttpRequestException("Spam algılandı");

                        post.Comments = post.Comments ?? new List<Comment>();

                        post.Comments.Add(new Comment
                        {
                            FirstAndLastName = model.FirstAndLastName,
                            Title = model.Title,
                            Content = model.Content,
                            Email = model.Email,
                            CreatedDate = DateTime.Now
                        });
                        _postService.Update(post);
                        TempData["Comment"] = "Yorum başarıyla eklendi";
                        _logger.LogInformation($"Yorum başarıyla eklendi {post.Title.ToUpper()}.");
                    }
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var comment = new ReplyComment
                        {                            
                            CommentId = model.MainId,
                            FirstAndLastName = model.FirstAndLastName,
                            Email = model.Email,
                            Content = model.Content,
                            CreatedDate = DateTime.Now
                        };

                        await _replyCommentService.AddAsync(comment);
                        TempData["ReplyComment"] = "Cevap başarıyla eklendi";
                        _logger.LogInformation($"Re-Yorum başarıyla eklendi {post.Title.ToUpper()}.");
                    }
                }
                return RedirectToAction("Details", "Post", new { id = post.Id, title = FriendlyUrllHelper.GenerateTitle(post.Title) });

            }
            catch (Exception ex)
            {
                _logger.LogError($"Yorum ekleme sırasında beklenmeyen hata {ex}.", ex);
                return Json(BadRequest());
            }
        }

        [Authorize]
        [Route("{controller}/{action}/{id}")]
        public async Task<IActionResult> EditPost(int id)
        {
            if (User.Identity!.IsAuthenticated)
            {
                var post = await _postService.GetPostIdAsync(id, false);
                ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(false), "Id", "CategoryName", post.CategoryId);
                var model = new UpdatePostViewModel()
                {
                    Id = post.Id,
                    Title = post.Title,
                    Description = post.Description,
                    CategoryId = post.CategoryId,
                    ExistingThumbnail = post.Thumbnail
                };

                if (post == null)
                    return NotFound();

                return View(model);
            }
            else
                return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{controller}/{action}/{id}")]
        public async Task<IActionResult> EditPost(int id, UpdatePostViewModel model)
        {
            try
            {
                var updatePost = await _postService.GetPostIdAsync(id, true);
                updatePost.Title = model.Title;
                updatePost.Description = model.Description;
                updatePost.CategoryId = model.CategoryId;
                ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(false), "Id", "CategoryName", updatePost.CategoryId);

                if (model.Thumbnail != null)
                {
                    var extension = "Jpeg";
                    var newImageName = $"{model.Title}{Guid.NewGuid()}.{extension}";
                    string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/PostThumbnail", newImageName));
                    using (var image = await Image.LoadAsync(model.Thumbnail!.OpenReadStream()))
                    {
                        string newSize = _imageHelper.ImageResize(image, 1500, 700);
                        string[] sizeArray = newSize.Split(",");
                        image.Mutate(x => x.Resize(Convert.ToInt32(sizeArray[1]), Convert.ToInt32(sizeArray[0])));
                        await image.SaveAsJpegAsync(path);
                    }
                    updatePost.Thumbnail = newImageName;
                }

                PostValidation validations = new();

                ValidationResult result = await validations.ValidateAsync(updatePost);
                if (result.IsValid)
                {
                    _logger.LogInformation($"Post başarıyla güncellendi {updatePost.Title.ToUpper()}.");
                    _postService.Update(updatePost);
                    TempData["Update"] = "Başarıyla Güncellendi!";
                    return RedirectToAction("Details", "Post", new { id = updatePost.Id, title = FriendlyUrllHelper.GenerateTitle(updatePost.Title) });
                }
                else
                {
                    result.AddToModelState(this.ModelState);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Post güncelleme sırasında beklenmeyen hata {ex}.", ex);
                return BadRequest();
            }

        }

        [Route("post/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var post = await _postService.GetPostIdAsync(id);
                if (post == null)
                    return NotFound();
                await _postService.RemoveAsync(id);
                TempData["Message"] = $" {post.Title} başarıyla silindi!.";
                return RedirectToAction("GetPost", new { page = 1 });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Post silme sırasında beklenmeyen hata {ex}.", ex);
                return BadRequest();
            }
        }

        [Route("post/deletecomment")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                var post = await _commentService.GetByIdAsync(id);
                var postW = post.Post;
                if (post == null)
                    return NotFound();
                await _commentService.RemoveAsync(id);
                TempData["DeleteComment"] = "Yorum başarıyla silindi!";
                return RedirectToAction("Details", "Post", new { id = post.PostId, title = FriendlyUrllHelper.GenerateTitle(postW.Title) });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Yorum silme sırasında beklenmeyen hata {ex}.", ex);
                return BadRequest();
            }
        }

        public async Task<IActionResult> DeleteReplyComment(int id)
        {
            try
            {
                var replyComment = await _replyCommentService.GetByIdAsync(id);
                var postId = replyComment.Comment.Post.Id;
                var postTitle = replyComment.Comment.Post.Title;
                if (replyComment == null)
                    return NotFound();
                await _replyCommentService.RemoveAsync(replyComment.Id);
                TempData["DeleteReplyComment"] = "Yorum başarıyla silindi!";
                return RedirectToAction("Details", "Post", new { id = postId, title = FriendlyUrllHelper.GenerateTitle(postTitle) });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Re-yorum silme sırasında beklenmeyen hata {ex}.", ex);
                return BadRequest();
            }
        }
    }
}
