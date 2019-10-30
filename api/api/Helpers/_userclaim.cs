using api.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace api.Helpers
{
    public static class Userclaim
    {
        // Test function
        public static User getUserById(Claim claim) {
            var name = claim.Subject.FindFirst(n => n.Type == "name").Value;
            var expirationDate = claim.Subject.FindFirst(n => n.Type == "exp").Value;

            User _user = new User() { Id = claim.Value, Email = "test@test.com", Name = name, Password = "", Role = "", Token = "" };
            return _user;
        }
    }
}
