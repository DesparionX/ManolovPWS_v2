using ManolovPWS_v2.Api.Contracts.Projects;
using ManolovPWS_v2.Api.Maps;
using ManolovPWS_v2.Api.Services;
using ManolovPWS_v2.Modules.Projects.Project.Features.AddProject;
using ManolovPWS_v2.Modules.Projects.Project.Features.DeleteProject;
using ManolovPWS_v2.Modules.Projects.Project.Features.GetProjects;
using ManolovPWS_v2.Modules.Projects.Project.Features.UpdateProject;
using ManolovPWS_v2.Shared.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManolovPWS_v2.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectsController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var query = new GetAllProjectsQuery();
            var result = await _dispatcher.QueryAsync(query, cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken = default)
        {
            var query = new GetProjectQuery(id);
            var result = await _dispatcher.QueryAsync(query, cancellationToken);
            return result.ToActionResult();
        }

        [Authorize(Roles = Roles.Owner)]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddProjectRequest request, CancellationToken cancellationToken = default)
        {
            var command = new AddNewProjectCommand(
                Name: request.Name,
                Description: request.Description,
                ProjectState: request.ProjectState,
                LiveUrl: request.LiveUrl,
                GitHubUrl: request.GitHubUrl,
                GalleryPictures: request.GalleryPictures,
                ProjectStack: request.ProjectStack,
                ThumbUrl: request.ThumbUrl
                );

            var result = await _dispatcher.SendAsync(command, cancellationToken);
            return result.ToActionResult();
        }

        [Authorize(Roles = Roles.Owner)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id, CancellationToken cancellationToken = default)
        {
            var command = new RemoveProjectCommand(id);
            var result = await _dispatcher.SendAsync(command, cancellationToken);
            return result.ToActionResult();
        }

        [Authorize(Roles = Roles.Owner)]
        [HttpPut("{id}/state")]
        public async Task<IActionResult> ChangeState(string id, [FromBody] ChangeProjectStateRequest request, CancellationToken cancellationToken = default)
        {
            var command = new UpdateProjectStateCommand(
                ProjectId: id,
                NewState: request.NewState
                );

            var result = await _dispatcher.SendAsync(command, cancellationToken);
            return result.ToActionResult();
        }

        [Authorize(Roles = Roles.Owner)]
        [HttpPut("{id}/description")]
        public async Task<IActionResult> UpdateDescription(string id, [FromBody] UpdateProjectDescriptionRequest request, CancellationToken cancellationToken = default)
        {
            var command = new UpdateProjectDescriptionCommand(
                ProjectId: id,
                NewDescription: request.NewDescription
                );

            var result = await _dispatcher.SendAsync(command, cancellationToken);
            return result.ToActionResult();
        }

        [Authorize(Roles = Roles.Owner)]
        [HttpPut("{id}/gellery")]
        public async Task<IActionResult> UpdateGallery(string id, [FromBody] UpdateProjectGalleryRequest request, CancellationToken cancellationToken = default)
        {
            var command = new UpdateProjectGalleryCommand(
                ProjectId: id,
                NewGallery: request.NewGallery
                );

            var result = await _dispatcher.SendAsync(command, cancellationToken);
            return result.ToActionResult();
        }

        [Authorize(Roles = Roles.Owner)]
        [HttpPut("{id}/thumb")]
        public async Task<IActionResult> ChangeProjectThumb(string id, [FromBody] ChangeProjectThumbRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new UpdateProjectThumbCommand(
                ProjectId: id,
                NewThumb: request.NewThumb
                );

            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [Authorize(Roles = Roles.Owner)]
        [HttpPut("{id}/github-url")]
        public async Task<IActionResult> ChangeGitHubUrl (string id, [FromBody] ChangeProjectGitHubUrlRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new UpdateProjectGitHubUrl(
                ProjectId: id,
                NewGitHubUrl: request.NewUrl
                );

            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [Authorize(Roles = Roles.Owner)]
        [HttpPut("{id}/live-url")]
        public async Task<IActionResult> ChangeLiveUrl(string id, [FromBody] ChangeProjectLiveUrlRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new UpdateProjectLiveUrlCommand(
                ProjectId: id,
                NewLiveUrl: request.NewUrl
                );

            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [Authorize(Roles = Roles.Owner)]
        [HttpPut("{id}/name")]
        public async Task<IActionResult> ChangeName(string id, [FromBody] ChangeProjectNameRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new UpdateProjectNameCommand(
                ProjectId: id,
                NewName: request.NewName
                );

            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [Authorize(Roles = Roles.Owner)]
        [HttpPut("{id}/stack")]
        public async Task<IActionResult> UpdateStack(string id, [FromBody] UpdateProjectStackRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new UpdateProjectStackCommand(
                ProjectId: id,
                NewStack: request.NewStack
                );

            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }
    }
}
