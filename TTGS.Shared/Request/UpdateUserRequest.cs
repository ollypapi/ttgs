using System.Collections.Generic;

namespace TTGS.Shared.Request
{
    public class UpdateUserRequest : CreateUserRequest
    {
        public string  Id { get; set; }
        public List<string> DisabledRoles { get; set; }
    }
}
