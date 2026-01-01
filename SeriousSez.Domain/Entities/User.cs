using Microsoft.AspNetCore.Identity;

namespace SeriousSez.Domain.Entities
{
    public class User : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
