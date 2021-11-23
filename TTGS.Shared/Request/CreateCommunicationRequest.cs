using TTGS.Shared.Helper;

namespace TTGS.Shared.Request
{
    public class CreateCommunicationRequest
    {
        [SwaggerExclude]
        public string UserId { get; set; }
        public string RecipientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
