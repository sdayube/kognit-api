using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kognit.API.Domain.Common
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? LastModified { get; set; }
    }
}