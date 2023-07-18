using CleanArchitecture.Application.Features.AppOptions.Queries.GetAppOptionsByRolName;
using CleanArchitecture.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AdministrationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdministrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{rolname}", Name = "GetAppOptionsByRolName")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<AppOptionsViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<AppOptionsViewModel>>> GetAppOptionsByRolName(string rolname)
        {
            var query = new GetAppOptionsByRolNameQuery(rolname);
            var items = await _mediator.Send(query);
            await _mediator.Send(query);
            return Ok(items);
        }
    }
}
