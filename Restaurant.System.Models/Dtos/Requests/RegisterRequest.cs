using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Restaurant.System.Models.Dtos.Requests
{
    public class RegisterRequest
    {
        public string Register { get; set; }
        public string PasswordHash { get; set; }
        // Member
        public string CustomerNumber { get; set; }
        public bool IsEmail { get => Register.Contains('@'); }
    }
}