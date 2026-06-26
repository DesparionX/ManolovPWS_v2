using ManolovPWS_v2.Api.Contracts.Posts;
using ManolovPWS_v2.Api.Maps;
using ManolovPWS_v2.Api.Services;
using ManolovPWS_v2.Modules.Content.Post.Features.AddPost;
using ManolovPWS_v2.Modules.Content.Post.Features.DeletePost;
using ManolovPWS_v2.Modules.Content.Post.Features.EditPost;
using ManolovPWS_v2.Modules.Content.Post.Features.GetPosts;
using ManolovPWS_v2.Shared.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManolovPWS_v2.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostsController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var query = new GetAllPostsQuery();
            var result = await _dispatcher.QueryAsync(query, cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken = default)
        {
            var query = new GetPostByIdQuery(id);
            var result = await _dispatcher.QueryAsync(query, cancellationToken);
            return result.ToActionResult();
        }

        [Authorize(Roles = Roles.Owner)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewPostRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new AddNewPostCommand(
                Title: request.Title,
                Context: request.Context,
                Thumb: request.Thumb,
                Gallery: request.Gallery,
                IsPinned: request.IsPinned);

            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken = default)
        {
            var cmd = new RemovePostCommand(id);
            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [Authorize]
        [HttpPut("{id}/context")]
        public async Task<IActionResult> UpdateContext(string id, [FromBody] UpdatePostContextRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new EditPostContextCommand(
                PostId: id,
                NewContext: request.NewContext
            );

            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [Authorize]
        [HttpPut("{id}/title")]
        public async Task<IActionResult> UpdateTitle(string id, [FromBody] UpdatePostTitleRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new EditPostTitleCommand(
                PostId: id,
                NewTitle: request.NewTitle
            );

            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [Authorize]
        [HttpPut("{id}/thumb")]
        public async Task<IActionResult> UpdateThumb(string id, [FromBody] UpdatePostThumbRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new EditPostThumbCommand(
                PostId: id,
                NewThumb: request.NewThumb
            );

            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [Authorize]
        [HttpPut("{id}/gallery")]
        public async Task<IActionResult> UpdateGallery(string id, [FromBody] UpdatePostGalleryRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new EditPostGalleryCommand(
                PostId: id,
                NewGallery: request.NewGallery
            );

            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [Authorize(Roles = Roles.Owner)]
        [HttpPut("{id}/pin")]
        public async Task<IActionResult> PinPost(string id, CancellationToken cancellationToken = default)
        {
            var cmd = new PinPostCommand(id);
            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }
    }
}
