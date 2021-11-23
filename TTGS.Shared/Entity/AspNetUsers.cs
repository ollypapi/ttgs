using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TTGS.Shared.Entity
{
    public class AspNetUsers : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ParentUserId { get; set; }

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
