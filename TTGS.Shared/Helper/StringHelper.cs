using System;
using System.Collections.Generic;
using System.Text;

namespace TTGS.Common.Helper
{
    public static class StringHelper
    {
        public static string GenerateRandom()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", string.Empty);
            var date = DateTime.UtcNow.ToString("yyyyMMdd");
            return guid.Substring(0, 6) + "pQ-" + date;
        }

        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);
        }
    }
}
