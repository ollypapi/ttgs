using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTGS.Shared.Constants;
using TTGS.Web.Security;

namespace TTGS.Web.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [CustomAuthorization(UserRoleConstants.Admin, UserRoleConstants.Client)]
    public class ContractorController : ControllerBase
    {
        private IMediator _mediator;

        public ContractorController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
