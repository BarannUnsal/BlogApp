using FealtedByEsra.BAL.Abstract.Services.PostService;
using Microsoft.AspNetCore.Mvc;

namespace FealtedByEsra.PL.ViewComponents.FooterLastTwoPost
{
    public class LastTwoPost : ViewComponent
    {
        private readonly IPostService _postService;

        public LastTwoPost(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lastTwoPost = await _postService.TakeLastTwoPostAsync();
            return View(lastTwoPost);
        }
    }
}
