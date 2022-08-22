using FSE.SkillTracker.Application.Features.Profile.Commands;
using Microsoft.AspNetCore.Mvc;

namespace FSE.SkillTracker.UpdateProfileApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/engineer")]
    [ApiController]
    public class UpdateProfileController : BaseApiController
    {
        [HttpPut("update-profile/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(Guid userId, UpdateProfileCommand command)
        {
            if (userId != command.UserId)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}
