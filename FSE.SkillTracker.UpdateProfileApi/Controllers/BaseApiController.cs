﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FSE.SkillTracker.UpdateProfileApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
