using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public double Amount { get; set; }
        public SaleStatus Status { get; set; }
        public Seller Seller { get; set; }

        public SalesRecord()
        {
        }

        public SalesRecord(int id, DateTime createdAt, double amount, SaleStatus status, Seller seller)
        {
            Id = id;
            CreatedAt = createdAt;
            Amount = amount;
            Status = status;
            Seller = seller;
        }
    }
}
