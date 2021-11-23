namespace TTGS.Shared.Request
{
    public class CreateOrderRequest
    {
        public string Commodity { get; set; }
        public string OwnerId { get; set; }
        public string Quantity { get; set; }
        public string Exporter { get; set; }
        public string Importer { get; set; }
        public int NumberOfTruck { get; set; }
        public string PickupPoints { get; set; }
        public string DropOffPoints { get; set; }
        public string TransitBorders { get; set; }
        public string ImportBorder { get; set; }
        public AgentDetailsRequest ExportAgentDetails { get; set; }
        public AgentDetailsRequest ImportAgentDetails { get; set; }
    }

    public class AgentDetailsRequest
    {
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumbers { get; set; }
        public string ContactPerson { get; set; }
    }
}
