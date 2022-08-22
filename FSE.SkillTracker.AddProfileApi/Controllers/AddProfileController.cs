using FSE.SkillTracker.Application.Features.Profile.Commands;
using Microsoft.AspNetCore.Mvc;

namespace FSE.SkillTracker.AddProfileApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/engineer")]
    [ApiController]
    public class AddProfileController : BaseApiController
    {
        [HttpPost("add-profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProfile([FromBody] CreateProfileCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        
    }
}
