namespace TTGS.Shared.Response
{
    public class OrderStatusStatsResponse
    {
        public int OrderReceived { get; set; }
        public int OrderLoaded { get; set; }
        public int OrderDelivered { get; set; }
        public int OrderInProgress { get; set; }
    }
}
