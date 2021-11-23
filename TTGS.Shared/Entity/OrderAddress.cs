using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace TTGS.Shared.Entity
{
    [Owned]
    public class OrderAddress
    {
        [Key]
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumbers { get; set; }
        public string ContactPerson { get; set; }
        public string AddressType { get; set; }
    }
}
