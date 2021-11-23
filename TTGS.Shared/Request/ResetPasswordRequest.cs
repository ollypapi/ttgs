using System;
using System.Collections.Generic;
using System.Text;
using TTGS.Shared.Entity;

namespace TTGS.Shared.Request
{
    public class ResetPasswordRequest
    {
        public string  Id { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
