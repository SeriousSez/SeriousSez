using System;

namespace SeriousSez.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }

        public BaseEntity()
        {
            Created = DateTime.Now;
        }
    }
}
