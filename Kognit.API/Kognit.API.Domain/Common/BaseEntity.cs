using System;

namespace Kognit.API.Domain.Common
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }
    }
}