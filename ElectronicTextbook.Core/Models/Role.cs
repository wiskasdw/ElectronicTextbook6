using ElectronicTextbook.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ElectronicTextbook.Core.Models
{
    public class Role : IdentityRole<string>
    {
        public ICollection<User> Users { get; set; }
        public RoleType RoleType { get; set; } // Add this property
    }

}