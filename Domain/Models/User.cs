using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public abstract class User
    {
        public int Id { get;  set; }
        public string Username { get;  set; }
        public string Email {  get; set; }
        public string Password { get;  set; }
        public Role Role { get;  set; }
        public bool IsVerified { get;  set; } = false;
        public string VerificationCode { get;  set; }
        public DateTime VerificationExpiry { get; set; }
        public decimal Balance { get; set; }
        public DateTime? LastLogin { get; set; }
     
    }
}
