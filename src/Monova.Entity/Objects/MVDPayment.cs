using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monova.Entity
{
    [Table("Payment")]
    public class MVDPayment
    {
        [Key]
        public Guid PaymentId { get; set; }
        public Guid UserId { get; set; }
        public Guid SubscriptionId { get; set; }
        public string Provider { get; set; }
        public string Token { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
    }
}