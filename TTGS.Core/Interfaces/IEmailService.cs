using TTGS.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TTGS.Core.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string fromEmail, string message, string fullName, string subject, string toEmail = null);
    }
}
