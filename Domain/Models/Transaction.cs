using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
    }
}
