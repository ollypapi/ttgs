using System;
using System.Collections.Generic;
using System.Text;

namespace TTGS.Shared.Response
{
    public class SearchUserResponse
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
