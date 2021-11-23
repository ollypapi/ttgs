using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TTGS.Web.Security;
using System;
using System.Threading.Tasks;
using TTGS.Core.Commands;
using TTGS.Core.Interfaces;
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
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;

        public UserController(ILogger<UserController> logger, IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _logger = logger;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest model)
        {
            var response = await _userService.Authenticate(model, IPAddress());

            if (response == null)
                return Unauthorized(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Headers["refreshToken"];
            var response = await _userService.RefreshToken(refreshToken, IPAddress());

            if (response == null)
                return Unauthorized(new { message = "Invalid token" });

            return Ok(response);
        }

        private string IPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }


        [HttpPost("create")]
        [CustomAuthorization(UserRoleConstants.Admin)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest createUserRequest)
        {
            var isCreated = await _mediator.Send(new CreateUserCommand { User = createUserRequest, Commit = true });
            return Ok(isCreated);
        }


        [HttpGet("list")]
        [CustomAuthorization(UserRoleConstants.Admin)]
        public async Task<IActionResult> ListUsers([FromQuery] PagingParameters userParameters)
        {
            var users = await _mediator.Send(new ListUserQuery { Parameters = userParameters });
            HttpHeaderHelper.AddPagingMetaHeader(Response, users);
            return Ok(users);
        }

        [HttpGet("searchuser")]
        [CustomAuthorization(UserRoleConstants.Admin)]
        public async Task<IActionResult> SearchUsers([FromQuery] SearchUserRequest userParameters)
        {
            var users = await _mediator.Send(new SearchUserQuery { Parameters = userParameters });
            HttpHeaderHelper.AddPagingMetaHeader(Response, users);
            return Ok(users);
        }

        [HttpGet("get/{id}")]
        [CustomAuthorization(UserRoleConstants.Admin)]
        public async Task<IActionResult> GetUser(string id)
        {
            var users = await _mediator.Send(new UserQuery { Id = id });
            return Ok(users);
        }

        [HttpPut("update")]
        [CustomAuthorization(UserRoleConstants.Admin)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest updateUserRequest)
        {
            var isUpdated = await _mediator.Send(new UpdateUserCommand { User = updateUserRequest });
            return Ok(isUpdated);
        }

        [HttpPost("forgotpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var resetUrl = Url.Link("GetResetPassword", new {email = request.UserRegisteredEmail });
            var result = await _mediator.Send(new GetUserByEmailQuery { Parameters = request, ResetUrl = resetUrl });
            return Ok(result);
        }

        [HttpPost]
        [Route("reset-password", Name = "GetResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
           
            var result = await _mediator.Send(new UpdateUserPasswordCommand { ResetPasswordRequest = request});
            return Ok(result);
        }
    }
}
