using FealtedByEsra.BAL.Abstract.Services.PostService;
using Microsoft.AspNetCore.Mvc;

namespace FealtedByEsra.PL.ViewComponents.TopThreePost
{
    public class TopThreePost : ViewComponent
    {
        private readonly IPostService _postService;

        public TopThreePost(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var posts = await _postService.TopThreePostAsync();
            return View(posts);
        }
    }
}
