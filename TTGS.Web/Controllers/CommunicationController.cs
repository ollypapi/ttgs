using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TTGS.Web.Security;
using System;
using System.Threading.Tasks;
using TTGS.Core.Commands;
using TTGS.Core.Queries;
using TTGS.Shared.Constants;
using TTGS.Shared.DTOs;
using TTGS.Shared.Helper;
using TTGS.Shared.Request;

namespace TTGS.Web.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [CustomAuthorization(UserRoleConstants.Admin, UserRoleConstants.Client)]
    public class CommunicationController : ControllerBase
    {
        private readonly ILogger<CommunicationController> _logger;
        private readonly IMediator _mediator;

        public CommunicationController(ILogger<CommunicationController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCommunication([FromBody] CreateCommunicationRequest createCommunicationRequest)
        {
            createCommunicationRequest.UserId = User.GetUserId();
            var communication = await _mediator.Send(new CreateCommunicationCommand
            {
                Communication = createCommunicationRequest,
                IsCreatedByAdmin = User.IsInRole(UserRoleConstants.Admin),
                Commit = true
            });
            return Ok(communication);
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListCommunications([FromQuery] PagingParameters commnunicationParameters)
        {
            var communication = await _mediator.Send(new ListCommunicationQuery
            {
                Parameters = commnunicationParameters,
                UserId = Guid.Parse(User.GetUserId())
            });
            HttpHeaderHelper.AddPagingMetaHeader(Response, communication);
            return Ok(communication);
        }
    }
}
