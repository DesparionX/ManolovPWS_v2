using ManolovPWS_v2.Api.Contracts.Profile;
using ManolovPWS_v2.Api.Maps;
using ManolovPWS_v2.Api.Services;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.User.Features.GetUser;
using ManolovPWS_v2.Modules.Identity.User.Features.UpdateUser;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManolovPWS_v2.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController(ICurrentUser<UserId> currentUser, IDispatcher dispatcher) : ControllerBase
    {
        private readonly ICurrentUser<UserId> _currentUser = currentUser;
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet("me")]
        public async Task<IActionResult> LoadProfile()
        {
            var id = _currentUser.Id.ToString();
            var q = new GetUserPrivateProfileQuery(id);
            var result = await _dispatcher.QueryAsync(q);

            return result.ToActionResult();
        }

        [HttpPut("name")]
        public async Task<IActionResult> UpdateName([FromBody] UpdateNameRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new UpdateNameCommand(
                FirstName: request.FirstName,
                LastName: request.LastName,
                MiddleName: request.MiddleName
                );

            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut("email")]
        public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new UpdateEmailCommand(request.Email);
            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut("address")]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new UpdateAddressCommand(
                Country: request.Country,
                Region: request.Region,
                Municipality: request.Municipality,
                City: request.City,
                Street: request.Street,
                PostalCode: request.PostalCode
                );

            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut("birth-date")]
        public async Task<IActionResult> UpdateBirthDate([FromBody] UpdateBirthDateRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new UpdateBirthDateCommand(request.BirthDate);
            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut("gender")]
        public async Task<IActionResult> UpdateGender([FromBody] UpdateGenderRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new UpdateGenderCommand(request.Gender);
            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut("phone-number")]
        public async Task<IActionResult> UpdatePhoneNumber([FromBody] UpdatePhoneNumberRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new UpdatePhoneNumberCommand(request.PhoneNumber);
            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut("profession")]
        public async Task<IActionResult> UpdateProfession([FromBody] UpdateProfessionRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new UpdateProfessionCommand(request.Profession);
            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut("summary")]
        public async Task<IActionResult> UpdateSummary([FromBody] UpdateSummaryRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new UpdateSummaryCommand(request.Summary);
            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut("profile-picture")]
        public async Task<IActionResult> UpdateProfilePicture([FromBody] UpdateProfilePictureRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new UpdateProfilePictureCommand(request.ProfilePictureUrl);
            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut("experience")]
        public async Task<IActionResult> UpdateExperience([FromBody] UpdateExperienceRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new UpdateExperienceCommand(request.Experience);
            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut("education")]
        public async Task<IActionResult> UpdateEducation([FromBody] UpdateEducationRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new UpdateEducationHistoryCommand(request.Education);
            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut("skills")]
        public async Task<IActionResult> UpdateSkills([FromBody] UpdateSkillsRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new UpdateSkillsCommand(request.Skills);
            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut("languages")]
        public async Task<IActionResult> UpdateLanguages([FromBody] UpdateLanguagesRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new UpdateLanguagesCommand(request.Languages);
            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut("certificates")]
        public async Task<IActionResult> UpdateCertificates([FromBody] UpdateCertificatesRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new UpdateCertificatesCommand(request.Certificates);
            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut("contacts")]
        public async Task<IActionResult> UpdateContacts([FromBody] UpdateContactsRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new UpdateContactsCommand(request.Contacts);
            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }
    }
}

