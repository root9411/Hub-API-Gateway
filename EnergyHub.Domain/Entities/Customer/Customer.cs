using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergyHub.Domain.Entities.Customer
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(300)]
        public string LastName { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        [Required]
        [MaxLength(-1)] // -1 represents MAX in SQL Server for NVARCHAR(MAX)
        public string ClientSecret { get; set; }

        [Required]
        [MaxLength(-1)] // -1 represents MAX in SQL Server for NVARCHAR(MAX)
        public string EnvironmentDatabaseName { get; set; }

        [MaxLength(-1)] // -1 represents MAX in SQL Server for NVARCHAR(MAX)
        public string AccessToken { get; set; }

        [MaxLength(-1)] // -1 represents MAX in SQL Server for NVARCHAR(MAX)
        public string RefreshToken { get; set; }

        public DateTime accessTokenExpirationTime { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
