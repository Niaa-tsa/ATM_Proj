using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class LoanRequest
    {
   
            public int Id { get; set; }

            public int UserId { get; set; }

            public decimal Amount { get; set; }

            public string Status { get; set; } = "Pending";
        }
    }

