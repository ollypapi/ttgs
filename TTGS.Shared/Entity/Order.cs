using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TTGS.Shared.Entity
{
    public class Order
    {
        public Order()
        {
            Addresses = new List<OrderAddress>();
        }

        [Key]
        public long Id { get; set; }
        public string OwnerId { get; set; }
        public string Commodity { get; set; }
        public string Quantity { get; set; }
        public string Exporter { get; set; }
        public string Importer { get; set; }
        public int NumberOfTruck { get; set; }
        public string PickupPoints { get; set; }
        public string DropOffPoints { get; set; }
        public string TransitBorders { get; set; }
        public string ImportBorder { get; set; }
        public List<OrderAddress> Addresses { get; set; }
        public DateTime DateCreated { get; set; }
        public string OrderStatus { get; set; }
    }
}
