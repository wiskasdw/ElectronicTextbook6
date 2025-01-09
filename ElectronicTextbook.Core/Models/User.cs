using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
namespace ElectronicTextbook.Core.Models
{
    public class User : IdentityUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
