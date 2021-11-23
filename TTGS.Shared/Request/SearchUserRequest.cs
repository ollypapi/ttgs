using System;
using System.Collections.Generic;
using System.Text;
using TTGS.Shared.DTOs;

namespace TTGS.Shared.Request
{
    public class SearchUserRequest : PagingParameters
    {
        public string searchParams { get; set; }
    }
}
