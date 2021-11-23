using System;

namespace TTGS.Shared.Response
{
    public class ListCommunicationResponse
    {
        public string Message { get; set; }
        public bool IsCreatedByAdmin { get; set; }
        public string Subject { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
