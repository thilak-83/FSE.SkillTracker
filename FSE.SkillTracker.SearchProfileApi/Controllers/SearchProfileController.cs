using FSE.SkillTracker.Application.Features.Profile.Queries;
using FSE.SkillTracker.SearchProfileApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace FSE.SkillTracker.UpdateProfileApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin")]
    [ApiController]
    public class SearchProfileController : BaseApiController
    {
        [HttpGet("{criteria}/{criteriaValue}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string criteria, string criteriaValue)
        {
            return Ok(await Mediator.Send(new GetProfilesByCriteriaQuery() { Criteria = criteria, CriteriaValue = criteriaValue }));
        }
    }
}
