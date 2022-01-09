using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models.DTOs
{
    public class AuthenticateResponse
    {
        public string Emial { get; set; }
        public Guid AccountId { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpirationDate { get; set; }
    }
}
