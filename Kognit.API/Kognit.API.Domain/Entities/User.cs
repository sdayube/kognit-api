using Kognit.API.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Kognit.API.Domain.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(11)]
        public string CPF { get; set; }
    }
}