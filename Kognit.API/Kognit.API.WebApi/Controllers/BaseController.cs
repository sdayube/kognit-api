﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Kognit.API.WebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}