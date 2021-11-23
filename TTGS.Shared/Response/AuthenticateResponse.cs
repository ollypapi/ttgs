using Newtonsoft.Json;
using TTGS.Shared.Entity;

namespace TTGS.Shared.Response
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string[] Roles { get; set; }

        public AuthenticateResponse(AspNetUsers user, string accessToken, string refreshToken, string[] roles)
        {
            Id = user.Id;
            Username = user.UserName;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            Roles = roles;
        }
    }
}
