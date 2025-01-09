using ElectronicTextbook.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ElectronicTextbook.Core.Models
{
    public class User : IdentityUser<string>
    {
        public ICollection<Lecture> Lectures { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }

        // Example method to check user role
        public bool IsAdmin()
        {
            return Role?.RoleType == RoleType.Admin;
        }
    }

}