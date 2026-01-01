using System;

namespace SeriousSez.Domain.Entities
{
    public class UserSeeker
    {
        public Guid Id { get; set; }
        public string IdentityId { get; set; }
        public User Identity { get; set; }  // navigation property
        public string Location { get; set; }
    }
}
