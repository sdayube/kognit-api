using Kognit.API.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Kognit.API.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        [Key]
        public override Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Value { get; set; }

        [Required]
        [MaxLength(50)]
        public string BankName { get; set; }

        public User User { get; set; }
    }
}