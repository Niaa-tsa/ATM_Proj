using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public abstract class User
    {
        public int Id { get; protected set; }
        public string Username { get; protected set; }
        public string Password { get; protected set; }
        public Role Role { get; protected set; }
    }
}
