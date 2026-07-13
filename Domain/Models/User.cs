using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public  class User
    {
        public int Id { get;  set; }
        public string Username { get;  set; }
        public string Password { get;  set; }
        public Role Role { get;  set; }
        public bool IsVerified { get;  set; }
        public string VerificationCode { get;  set; }
    }
}
