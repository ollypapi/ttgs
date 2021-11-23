using Newtonsoft.Json;
using System.Collections.Generic;
using TTGS.Shared.Entity;

namespace TTGS.Shared.Response
{
    public class ListUserResponse
    {
        public ListUserResponse()
        {
            Roles = new List<string>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }

        [JsonIgnore]
        public AspNetUsers User { get; set; }
    }
}
