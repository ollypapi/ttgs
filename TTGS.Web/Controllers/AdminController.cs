using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TTGS.Web.Security;
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
    [CustomAuthorization(UserRoleConstants.Admin)]
    public class AdminController : ControllerBase
    {
        private IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

       
    }
}
