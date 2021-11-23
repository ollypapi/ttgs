using Newtonsoft.Json;
using System;

namespace TTGS.Shared.Response
{
    public class OrderIdsResponse
    {
        public long Id { get; set; }
        [JsonIgnore]
        public DateTime DateCreated { get; set; }
    }
}
